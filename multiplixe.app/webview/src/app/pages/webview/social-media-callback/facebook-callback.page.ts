import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppInjector } from 'src/app/app-injector';
import { BasePage } from '../../base-page';
import { FacebookService } from 'src/app/services/social-media/facebook.service';

@Component({
  selector: 'app-facebook-callback',
  templateUrl: './social-media-callback.page.html'
})
export class FacebookCallbackPage extends BasePage implements OnInit {

  private facebookService: FacebookService;

  constructor(private route: ActivatedRoute) {
    super();

    this.facebookService = AppInjector.get(FacebookService)

  }

  async ngOnInit() {
    super.ngOnInit();

    this.route.queryParams.subscribe(params => {
      this.process(params['code'])
    });    
  }

  async process(code: string) {

    try {

      this.runLoading();

      await this.facebookService.process(code);
    }
    catch (e) {
      await this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Facebook. Por favor, tente novamente mais tarde.", async () => { this.process(code) });
    }
    finally {
      this.stopLoading();
      super.redirect('webview/social-media-connection/facebook')
    }
  }
}
