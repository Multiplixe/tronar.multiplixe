import Entry from '../entries/entry';
import { EntryHelper } from '../entries/entry.helper';
import { AppInjector } from '../app-injector'
import { LoaderService } from '../services/loader.service';
import { ToastService } from '../services/toast.service';
import { ErrorLogService } from '../services/error-log.service';
import { RouterRedirectService } from '../services/router-redirect.service';
import { OnInit, NgZone } from '@angular/core';
import { HttpStatusCode } from '../enums/http-status.enum';
import { AuthService } from '../services/auth.service';
import { DisplayControl } from '../dtos/display-control';
import { AngularFireAuth } from '@angular/fire/auth';

export class BasePage implements OnInit {

    public loaderService: LoaderService;
    public toastService: ToastService;
    public errorLogService: ErrorLogService;
    public routerRedirectService: RouterRedirectService;
    public authService: AuthService;
    private angularFireAuth: AngularFireAuth;
    public zone: NgZone;

    public displayControl: DisplayControl = DisplayControl.create();

    constructor() {
        this.loaderService = AppInjector.get(LoaderService)
        this.toastService = AppInjector.get(ToastService)
        this.errorLogService = AppInjector.get(ErrorLogService)
        this.routerRedirectService = AppInjector.get(RouterRedirectService)
        this.authService = AppInjector.get(AuthService)
        this.angularFireAuth = AppInjector.get(AngularFireAuth)
        this.zone = AppInjector.get(NgZone);
    }

    ngOnInit() {

        this.displayControl.reset();
        this.stopLoading();
    }

    isInvalidStatus(dto: Entry): boolean {
        return EntryHelper.isInvalidStatus(dto);
    }

    isValidStatus(dto: Entry): boolean {
        return EntryHelper.isValidStatus(dto);
    }

    isInvalidError(dto: Entry): boolean {
        return EntryHelper.isInvalidError(dto);
    }

    isAlreadyError(dto: Entry): boolean {
        return EntryHelper.isAlreadyError(dto);
    }

    isDifferentCode(dto: Entry): boolean {
        return EntryHelper.isDifferentCode(dto);
    }

    redirect(route: string) {
        this.routerRedirectService.redirect(route);
    }

    redirectRestrict(route: string) {
        this.routerRedirectService.redirectRestrict(route);
    }


    navigateForwardRestrict(route: string) {
        this.routerRedirectService.navigateForwardRestrict(route);
    }

    canShowContent() {
        this.displayControl.set(1);
    }

    showContent() {
        return this.displayControl.is(1);
    }

    async showToast(text: string) {
        return this.toastService.show(text);
    }

    async runLoading(texto: string = null) {
        this.zone.run(async () => {
            await this.loaderService.run(texto);
        });
    }

    async stopLoading() {
        this.zone.run(async () => {
            await this.loaderService.stop();
        });
    }

    async errorLog(error: any) {
        return this.errorLogService.log(error);
    }

    async processError(error: any, message: string, callback: Function = null) {

        if (error && error.status === HttpStatusCode.unauthorized) {

            this.authService.refreshTokenProcess()
                .then(() => {
                    if (callback) {
                        callback();
                    }
                })
                .catch(() => {
                    this.authService.logoff();
                    this.routerRedirectService.redirect('login');
                });

        }
        else {
            this.showToast(message)
            await this.errorLogService.log(error);
        }

    }

    async updateCurrentUser(u: firebase.User) {
        await u.reload();
        await this.angularFireAuth.updateCurrentUser(u);
    }

}
