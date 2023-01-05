import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

import { User } from './user.model';
import { AuthenticationDetails, CognitoUser, CognitoUserAttribute, CognitoUserPool, CognitoUserSession } from 'amazon-cognito-identity-js'

const POOL_DATA = {
  UserPoolId : "us-east-1_5C5vjLamY",
  ClientId:"59jk3ak1nqc8j4b1lisig0i3aa"
}

const userPool = new CognitoUserPool(POOL_DATA);

@Injectable()
export class AuthService {
  authIsLoading = new BehaviorSubject<boolean>(false);
  authDidFail = new BehaviorSubject<boolean>(false);
  authStatusChanged = new Subject<boolean>();
  
  constructor(private router : Router) {}
  signUp(username: string, email: string, password: string, name : string): void {
    this.authIsLoading.next(true);
    const user: User = {
      name:name,
      username: username,
      email: email,
      password: password
    };
    
    const emailAttribute = {
      Name: 'email',
      Value: user.email
    };

    const attrList : CognitoUserAttribute[] = [];
    const validationList : CognitoUserAttribute[] = [];

    attrList.push(new CognitoUserAttribute(emailAttribute));

    var that = this;
    userPool.signUp(user.username, user.password, attrList, validationList, function(
      err,
      result : any
    ) {
      if (err) {
        that.authDidFail.next(true)
        that.authIsLoading.next(false)
        console.log(err.message || JSON.stringify(err));
        return;
      }
      that.authDidFail.next(false)
      that.authIsLoading.next(false)
      var cognitoUser = result.user;
      console.log('user name is ' + cognitoUser.getUsername());
    });
    return;
  }
  confirmUser(username: string, code: string) {
    var that = this;
    this.authIsLoading.next(true);
    var userData = {
      Username: username,
      Pool: userPool,
    };

    var router = this.router
    
    var cognitoUser = new CognitoUser(userData);
    cognitoUser.confirmRegistration(code, true, function(err, result) {
      if (err) {
        that.authDidFail.next(true)
        that.authIsLoading.next(false)
        console.log(err.message || JSON.stringify(err));
        return;
      }
      else
      {
        that.authDidFail.next(false)
        that.authIsLoading.next(false)
      router.navigate(['/']);
      console.log('call result: ' + result);
      }
    });
  }
  signIn(username: string, password: string): void {
    var that = this;
    this.authIsLoading.next(true);
    const authData = {
      Username: username,
      Password: password
    };

    const authDetails = new AuthenticationDetails(authData);
    var userData = {
      Username: username,
      Pool: userPool,
    };
    var cogUser = new CognitoUser(userData);
    cogUser.authenticateUser(authDetails,  {
       onSuccess(result: CognitoUserSession){
        that.authStatusChanged.next(true)
        that.authDidFail.next(false)
        that.authIsLoading.next(false)
        console.log(result);
       },
       onFailure(err){
        that.authDidFail.next(true)
        that.authIsLoading.next(false)
        console.log(err);
       }
    } )
    this.authStatusChanged.next(true);
    return;
  }
  getAuthenticatedUser() {
    return userPool.getCurrentUser();
  }
  logout() {
    this.getAuthenticatedUser()?.signOut();
    this.authStatusChanged.next(false);
  }
  isAuthenticated(): Observable<boolean> {
    const user = this.getAuthenticatedUser();
    const obs = Observable.create((observer : any) => {
      if (!user) {
        observer.next(false);
      } else {
        user.getSession((err : any, session: any) =>{
          if(err)
          {
            observer.next(false);
          }
          else
          {
            if(session.isValid())
            {
              observer.next(true);
            }
            else
            {
              observer.next(false);
            }
          }
        } )
      }
      observer.complete();
    });
    return obs;
  }
  initAuth() {
    this.isAuthenticated().subscribe(
      (auth) => this.authStatusChanged.next(auth)
    );
  }
}
