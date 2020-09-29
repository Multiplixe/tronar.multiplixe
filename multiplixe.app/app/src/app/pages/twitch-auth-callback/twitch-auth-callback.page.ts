import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TwitchAccessTokenDto } from 'src/app/dtos/twitch-access-token.dto';
import { TwitchService } from 'src/app/services/twitch.service';
import { AppInjector } from 'src/app/app-injector';
import { RestrictPage } from '../restrict.page';
import { DataTransferService } from 'src/app/services/data-transfer.service';

@Component({
  selector: 'app-twitch-auth-callback',
  templateUrl: './twitch-auth-callback.page.html'
})
export class TwitchAuthCallbackPage extends RestrictPage implements OnInit {

  private twitchService: TwitchService;
  private dataTransferService: DataTransferService;

  constructor(private route: ActivatedRoute) {
    super();

    this.twitchService = AppInjector.get(TwitchService);
    this.dataTransferService = AppInjector.get(DataTransferService);

    this.route.queryParams.subscribe(params => {
      this.process(params['code']);
    });
  }

  async ngOnInit() {
    super.ngOnInit();
  }

  async process(code: string) {

    try {

      this.runLoading();

      var parameters = new TwitchAccessTokenDto(code);

      await this.twitchService.process(parameters);

    }
    catch (e) {
      this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Twitch. Por favor, tente novamente mais tarde.", async () => { this.process(code) });
    }
    finally {
      this.stopLoading();

      this.dataTransferService.set({ redirect: 'minhas-conexoes/twitch' });

      this.redirect('setup');
    }
  }
}
