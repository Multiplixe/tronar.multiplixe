import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Injectable, Inject } from "@angular/core";

import { AppInjector } from '../app-injector';
import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthGuard implements CanActivate {

    private authService: AuthService;

    constructor(
        private _router: Router) {
        this.authService = AppInjector.get(AuthService)
    }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Promise<boolean> {

        return new Promise<boolean>((resolve, reject) => {

            let hasToken = this.authService.hasToken();

            if (hasToken) {
                resolve();
            }
            else {
                console.log("COMUNICAR APP SOBRE FALTA DE TOKEN")
                reject();
            }
        });
    }
}
