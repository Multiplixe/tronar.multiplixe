import { Component, OnInit } from "@angular/core";
import { AppInjector } from '../app-injector';
import { HttpStatusCode } from '../enums/http-status.enum';
import { DisplayHelper } from '../helpers/display.helper';
import { LoaderService } from '../services/loader.service';
import { RouterRedirectService } from '../services/router-redirect.service';

@Component({
  selector: 'app-base-page',
  template: ''
})
export class BasePage implements OnInit {

  public loaderService: LoaderService;
  public display: DisplayHelper = DisplayHelper.create();
  public routerRedirectService: RouterRedirectService;

  constructor() {
    this.loaderService = AppInjector.get(LoaderService)
    this.routerRedirectService = AppInjector.get(RouterRedirectService)
  }

  ngOnInit() {
    this.display.reset();
    this.stopLoading();
  }

  canShowContent() {
    this.display.set(1);
  }

  showContent() {
    return this.display.is(1);
  }

  async runLoading(texto: string = null) {
    await this.loaderService.run(texto);
  }

  async stopLoading() {
    await this.loaderService.stop();
  }

  redirect(route: string, params: any = {}) {
    this.routerRedirectService.redirect(route, params);
  }

  showMessage(text: string) {

  }

  async processError(error: any, message: string, callback: Function = null) {

    if (error && error.status === HttpStatusCode.unauthorized) {

    }
    else {
      this.showMessage(message)
    }

  }

}