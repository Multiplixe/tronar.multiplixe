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

    this.route.queryParams.subscribe(queryParams => {
      this.route.params.subscribe(params => {
        this.process(queryParams['code'], params["empresaId"], params["username"])
      });
    });    
  }

  async process(code: string, empresaId: string, username: string) {

    try {

      this.runLoading();

      await this.facebookService.process(code, empresaId, username);
    }
    catch (e) {
      await this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Facebook. Por favor, tente novamente mais tarde.", async () => { this.process(code, empresaId, username) });
    }
    finally {
      this.stopLoading();
      super.redirect('webview/social-media-connection/facebook')
    }
  }
}
