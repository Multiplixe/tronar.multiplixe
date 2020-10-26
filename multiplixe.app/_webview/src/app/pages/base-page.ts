import { OnInit } from "@angular/core";
import { AppInjector } from '../app-injector';
import { DisplayHelper } from '../helpers/display.helper';
import { LoaderService } from '../services/loader.service';

export class BasePage implements OnInit {

  public loaderService: LoaderService;
  public display: DisplayHelper = DisplayHelper.create();

  constructor() {
    this.loaderService = AppInjector.get(LoaderService)
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

}