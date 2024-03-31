import { Injectable } from '@angular/core';
import { User } from '../shared/user';


const USER_KEY : string = "auth-user";

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  clear(): void{
    window.sessionStorage.clear();
  }

  public saveUser(user: User): void {
    window.sessionStorage.removeItem( USER_KEY );
    window.sessionStorage.setItem( USER_KEY, JSON.stringify(user) );
  }

  public getUser(): any{
    var user = window.sessionStorage.getItem( USER_KEY );

    if( user ){
     return JSON.parse( user );
    }

     return {};
  }

  public isLoggedIn(): boolean{
    var user = window.sessionStorage.getItem( USER_KEY );

    if( user ){
     return true;
    }

    return false;
  }
}
