import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/tutorial.model';
import { AuthService } from '../components/user/auth.service';

const baseUrl = ' https://2u6ec472zj.execute-api.us-east-1.amazonaws.com/Dev/';


@Injectable({
  providedIn: 'root'
})
export class TutorialService {


  

  constructor(private http: HttpClient, private authService : AuthService) { }

  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
    }),
  };




  getAll(): Observable<Employee[]> {
    this.authService.getAuthenticatedUser()?.getSession((err:any, session:any) =>{
      if(err)
      {
        console.log(err);
        return;
      }
      else
      {
        this.httpOptions = {
          headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: session.getIdToken().getJwtToken(),
          }),
        };     
      }
    } )
    var url = baseUrl+"listemployee"
    return this.http.get<Employee[]>(url, this.httpOptions );
  }

  get(id: any): Observable<Employee> {
    this.authService.getAuthenticatedUser()?.getSession((err:any, session:any) =>{
      if(err)
      {
        console.log(err);
        return;
      }
      else
      {
        this.httpOptions = {
          headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: session.getIdToken().getJwtToken(),
          }),
        };     
      }
    } )

    var url = baseUrl+"employee"
    return this.http.get<Employee>(`${url}/?UserID=${id}`, this.httpOptions);
  }

  create(data: any): Observable<any> {
    this.authService.getAuthenticatedUser()?.getSession((err:any, session:any) =>{
      if(err)
      {
        console.log(err);
        return;
      }
      else
      {
        this.httpOptions = {
          headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: session.getIdToken().getJwtToken(),
          }),
        };     
      }
    } )
    var url = baseUrl+"employee"
    return this.http.post(url, data, this.httpOptions);
  }

  delete(id: any): Observable<any> {
    this.authService.getAuthenticatedUser()?.getSession((err:any, session:any) =>{
      if(err)
      {
        console.log(err);
        return;
      }
      else
      {
        this.httpOptions = {
          headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: session.getIdToken().getJwtToken(),
          }),
        };     
      }
    } )
    var url = baseUrl+"employee"
    return this.http.delete(`${url}/?UserID=${id}`, this.httpOptions);
  }

}