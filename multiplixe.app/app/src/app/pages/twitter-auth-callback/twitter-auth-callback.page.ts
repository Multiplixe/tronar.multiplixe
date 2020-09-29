import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TwitterAccessTokenDto } from 'src/app/dtos/twitter-access-token.dto';
import { TwitterService } from 'src/app/services/twitter.service';
import { AppInjector } from 'src/app/app-injector';
import { RestrictPage } from '../restrict.page';
import { DataTransferService } from 'src/app/services/data-transfer.service';

@Component({
  selector: 'app-twitter-auth-callback',
  templateUrl: './twitter-auth-callback.page.html'
})
export class TwitterAuthCallbackPage extends RestrictPage implements OnInit {

  private twitterService: TwitterService;
  private dataTransferService: DataTransferService;

  constructor(private route: ActivatedRoute) {
    super();

    this.twitterService = AppInjector.get(TwitterService)
    this.dataTransferService = AppInjector.get(DataTransferService);

    this.route.queryParams.subscribe(params => {
      this.accessToken(params['oauth_token'], params['oauth_verifier'])
    });
  }

  async ngOnInit() {
    super.ngOnInit();
  }

  async accessToken(token: string, verifier: string) {

    try {

      this.runLoading();

      var parameters = new TwitterAccessTokenDto(token, verifier);
      await this.twitterService.process(parameters);
    }
    catch (e) {
      this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Twitter. Por favor, tente novamente mais tarde.", async () => { this.accessToken(token, verifier) });
    }
    finally {
      this.stopLoading();

      this.dataTransferService.set({ redirect: 'minhas-conexoes/twitter' });

      this.redirect('setup');
    }
  }
}
