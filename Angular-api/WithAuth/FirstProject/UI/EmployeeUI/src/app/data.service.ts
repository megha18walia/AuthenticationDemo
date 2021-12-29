import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Employee } from "./employee";
import { MsalUserService } from "./msaluser.service";

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private url = environment.baseUrl + "api/Employee/Employees";

  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
    }),
  };

  constructor(private http: HttpClient, private msalService: MsalUserService) {}

  getEmployees(): Observable<Employee[]> {

    this.httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + this.msalService.GetAccessToken(),
      }),
    };

    return this.http.get(this.url, this.httpOptions).pipe((response: any) => {
      return response;
    });

    
  }
  getCurrentUserInfo() {
    this.msalService.getCurrentUserInfo();
  }

  logout() {
    this.msalService.logout();
  }

}
