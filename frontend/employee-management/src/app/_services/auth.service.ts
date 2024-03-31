import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { User } from '../shared/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseAPI:string = "https://localhost:44389/api/Authentication";

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders( { 'Content-Type': 'application/json' } )
  };


  login( userName: string, password: string ): Observable<any> {

     return this.http.post(
      this.baseAPI + '/login',
      JSON.stringify( {userName:userName, password:password} ),
      this.httpOptions
     );
    }

}
