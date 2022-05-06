import { Injectable } from '@angular/core';
import {  HttpRequest,  HttpHandler,  HttpEvent,  HttpInterceptor} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        //private logService: LogService,
    ) { }
    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
      ): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
          catchError((err) => {
            console.log(err);
            switch (err.status) {
              case 401:
                window.location.href='/unauthorized';
                break;            
              case 302:
                //this.authSrvice.authenticationService.logout();
                //this.router.navigate(['/login']);
                break;
              default:
                break;
            }
            if (err.status != 401 && err.status != 302) {
            //   this.notificationService.error({
            //     message:
            //       err.error == null
            //         ? err.message
            //         : err.error.message != undefined
            //         ? err.error.message
            //         : err.message,
            //     httpCode: err.error == null ? err.status : err.error.httpCode,
            //   });
            }
            return throwError(null);
          })
        );
      }
}