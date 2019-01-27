import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '../../../node_modules/@angular/common/http';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Account } from 'app/models/acccount';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})

export class AccountService {

  constructor(private http: HttpClient) { }

  baseUrl = environment.apiUrl + 'api/account/';
  tokenUrl = environment.apiUrl + 'connect/token';
  decodedToken: any;
  jwtHelper = new JwtHelperService()

  register(account: Account): Observable<Object> {
    return this.http.post(this.baseUrl + 'register', account);
  }

  login(account: Account): Observable<Object> {
    
    const body = new HttpParams()
    .set('client_id', 'resolver.web')
    .set('client_secret', 'secret')
    .set('grant_type', 'password')
    .set('username', account.username)
    .set('password', account.password);    
    
    return this.http.post(this.tokenUrl,
      body.toString(),
      {
        headers: new HttpHeaders()
          .set('Content-Type', 'application/x-www-form-urlencoded')
      }
    );
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout(): void {
    localStorage.removeItem('token');
  }
}

