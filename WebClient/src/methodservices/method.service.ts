import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";

import {  Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class MethodService {
  public headers = new HttpHeaders({
    'Content-Type': 'application/json',
    Accept: 'application/json',
  });
  options = { headers: this.headers };
  constructor(private http: HttpClient) {}

  Get(url: string): Observable<any> { 
    return  this.http.get<any>(`${environment.apiUrl}` + url, this.options).pipe(
      map((response: Response) => {
        debugger
        return response;
      },)
    );
  }
  handleError(error: HttpErrorResponse) {
    debugger
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
  Delete(url: string, code: any): Observable<any> {    
    return this.http
      .get<any>(`${environment.apiUrl}` + url + `${code}`, this.options)
      .pipe(
        map((response: Response) => {
          return response;
        })
      );
  }
  Post(url: string, model: any): Observable<any> {
   
    return this.http
      .post<any>(`${environment.apiUrl}` + url, model, this.options)
      .pipe(
        map((response: Response) => {
         
          return response;
        })
      );
  }

  FormData(url: string, model: any): Observable<any> {
    
    return this.http
      .post<any>(
        `${environment.apiUrl}` + url,
        model, //this.options
        {
          reportProgress: true,
        }
      )
      .pipe(
        map((response: Response) => {
          return response;
        })
      );
  }
  SubmitFormData(url: string, model: any): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}` + url, model).pipe(
      map((response: Response) => {
        return response;
      })
    );
  }
}
