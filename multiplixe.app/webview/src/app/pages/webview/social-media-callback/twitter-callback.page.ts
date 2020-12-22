import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppInjector } from 'src/app/app-injector';
import { BasePage } from '../../base-page';
import { TwitterService } from 'src/app/services/social-media/twitter.service';

@Component({
  selector: 'app-twitter-callback',
  templateUrl: './social-media-callback.page.html'
})
export class TwitterCallbackPage extends BasePage implements OnInit {

  private twitterService: TwitterService;

  constructor(private route: ActivatedRoute) {
    super();

    this.twitterService = AppInjector.get(TwitterService)

    this.route.queryParams.subscribe(queryParams => {
      this.route.params.subscribe(params => {
        this.process(queryParams['oauth_token'], queryParams['oauth_verifier'], params["username"])
      });
    });
  }

  async ngOnInit() {
    super.ngOnInit();
  }

  async process(token: string, verifier: string, username: string) {
    try {

      this.runLoading();

      await this.twitterService.process(token, verifier, username);

    }
    catch (e) {
      await this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Twitter. Por favor, tente novamente mais tarde.", async () => { this.process(token, verifier, username) });
    }
    finally {
      this.stopLoading();
      super.redirect('webview/social-media-connection/twitter')
    }
  }
}
