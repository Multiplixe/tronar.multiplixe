import { Component, OnInit } from '@angular/core';
import { RestrictPage } from './pages/restrict.page';
import { SetupService } from 'src/app/services/setup.service';
import { AppInjector } from 'src/app/app-injector';
import { DataTransferService } from './services/data-transfer.service';

@Component({
  selector: 'app-setup',
  templateUrl: './app.setup.component.html'
})
export class AppSetupComponent extends RestrictPage implements OnInit {

  public setupService: SetupService;
  private dataTransferService: DataTransferService;

  constructor() {
    super();

    this.setupService = AppInjector.get(SetupService);
    this.dataTransferService = AppInjector.get(DataTransferService);
  }

  async ngOnInit() {

    super.ngOnInit();

    await this.runLoading();

    await this.setupService.init();

      let data = this.dataTransferService.get();

      let url = 'dashboard';
  
      if (data && data.redirect) {
        url = data.redirect;
      }
  
      this.redirectRestrict(url);
  }

}
