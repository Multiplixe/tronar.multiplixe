import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppInjector } from 'src/app/app-injector';
import { BasePage } from '../../base-page';
import { TwitchService } from 'src/app/services/social-media/twitch.service';

@Component({
  selector: 'app-twitch-callback',
  templateUrl: './social-media-callback.page.html'
})
export class TwitchCallbackPage extends BasePage implements OnInit {

  private twitchService: TwitchService;

  constructor(private route: ActivatedRoute) {
    super();

    this.twitchService = AppInjector.get(TwitchService)

    this.route.queryParams.subscribe(queryParams => {
      this.route.params.subscribe(params => {
        this.process(queryParams['code'], params['username'])
      });
    });
  }

  async ngOnInit() {
    super.ngOnInit();
  }

  async process(code: string, username: string) {

    try {

      this.runLoading();

      await this.twitchService.process(code, username);

    }
    catch (e) {
      await this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Twitch. Por favor, tente novamente mais tarde.", async () => { this.process(code, username) });
    }
    finally {
      this.stopLoading();
      super.redirect('webview/social-media-connection/twitch')
    }
  }
}
