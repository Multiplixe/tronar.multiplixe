import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Observable } from "rxjs";

import { AuthService } from "../services/auth.service";
import { AppInjector } from '../app-injector';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    private authService: AuthService;

    constructor() {
        this.authService = AppInjector.get(AuthService)
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const token = this.authService.getToken();
        if (token) {
            request = request.clone({
                setHeaders: { Authorization: `bearer  ${token}` }
            });
        }

        return next.handle(request);
    }
}

