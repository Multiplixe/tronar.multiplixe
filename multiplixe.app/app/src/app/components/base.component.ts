import { LoaderService } from '../services/loader.service';
import { ToastService } from '../services/toast.service';
import { ErrorLogService } from '../services/error-log.service';
import { AppInjector } from '../app-injector';
import { RouterRedirectService } from '../services/router-redirect.service';
import { HttpStatusCode } from '../enums/http-status.enum';
import { Router } from '@angular/router';
import { EventEmitterService } from '../services/event-emitter.service';
import { Subscription } from 'rxjs';
import { OnDestroy, OnInit } from '@angular/core';
import { DisplayControl } from '../dtos/display-control';
import { EntryHelper } from '../entries/entry.helper';
import { Entry } from '../entries/entry';

export class BaseComponent implements OnInit, OnDestroy {

    public router: Router;
    public loaderService: LoaderService;
    public toastService: ToastService;
    public errorLogService: ErrorLogService;
    public routerRedirectService: RouterRedirectService;
    protected eventEmitter: EventEmitterService;
    private subscriptions: Subscription = new Subscription();
    public displayControl: DisplayControl = DisplayControl.create();
    
    constructor() {
        this.router = AppInjector.get(Router)
        this.loaderService = AppInjector.get(LoaderService)
        this.toastService = AppInjector.get(ToastService)
        this.errorLogService = AppInjector.get(ErrorLogService)
        this.routerRedirectService = AppInjector.get(RouterRedirectService)
        this.eventEmitter = AppInjector.get(EventEmitterService)
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
    async showToast(text: string) {
        return this.toastService.show(text);
    }

    async runLoading(texto: string = null) {
        return this.loaderService.run(texto);
    }

    async stopLoading() {
        return this.loaderService.stop();
    }

    async errorLog(error: any) {
        return this.errorLogService.log(error);
    }

    ngOnDestroy(): void {
        this.subscriptions.unsubscribe();
    }

    ngOnInit() {
        this.displayControl.reset();
        this.stopLoading();
    }    

    addSubscription(s: Subscription) {
        this.subscriptions.add(s);
    }    

    canShowContent() {
        this.displayControl.set(1);
    }

    showContent()
    {
        return this.displayControl.is(1);
    }      

    async processError(error: any) {
        if (error && error.status === HttpStatusCode.unauthorized) {
            this.stopLoading();
            this.router.navigate(["login"]);
        }
        else {
            await this.errorLogService.log(error);
        }
    }


}