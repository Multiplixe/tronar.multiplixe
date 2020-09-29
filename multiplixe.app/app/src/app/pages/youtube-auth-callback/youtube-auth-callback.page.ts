import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { YoutubeService } from 'src/app/services/youtube.service';
import { AppInjector } from 'src/app/app-injector';
import { RestrictPage } from '../restrict.page';
import { DataTransferService } from 'src/app/services/data-transfer.service';

@Component({
  selector: 'app-youtube-auth-callback',
  templateUrl: './youtube-auth-callback.page.html'
})
export class YoutubeAuthCallbackPage extends RestrictPage implements OnInit {

  private youtubeService: YoutubeService;
  private dataTransferService: DataTransferService;

  constructor(private route: ActivatedRoute) {
    super();

    this.youtubeService = AppInjector.get(YoutubeService);
    this.dataTransferService = AppInjector.get(DataTransferService);

    this.process();
  }

  async ngOnInit() {
    super.ngOnInit();
  }

  async process() {

    try {

      this.runLoading();

      let params = this.routerRedirectService.getParameterFromHash();

      await this.youtubeService.processToken(params);
    }
    catch (e) {
      this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Youtube. Por favor, tente novamente mais tarde.", async () => { this.process() });
    }
    finally {
      this.stopLoading();

      this.dataTransferService.set({ redirect: 'minhas-conexoes/youtube' });

      this.redirect('setup');
    }
  }
}
