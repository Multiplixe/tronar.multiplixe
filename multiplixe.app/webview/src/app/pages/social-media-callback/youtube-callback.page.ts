import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppInjector } from 'src/app/app-injector';
import { BasePage } from '../base-page';
import { YoutubeService } from 'src/app/services/social-media/youtube.service';

@Component({
  selector: 'app-youtube-callback',
  templateUrl: './social-media-callback.page.html'
})
export class YoutubeCallbackPage extends BasePage implements OnInit {

  private youtubeService: YoutubeService;

  constructor(private route: ActivatedRoute) {
    super();

    this.youtubeService = AppInjector.get(YoutubeService)
  }

  async ngOnInit() {
    super.ngOnInit();
    this.process()
  }

  async process() {

    try {

      this.runLoading();

      let params = this.routerRedirectService.getParameterFromHash();

      await this.youtubeService.process(params);

    }
    catch (e) {
      await this.processError(e, "Ocorreu algum problema ao conectar nosso sistema ao Youtube. Por favor, tente novamente mais tarde.", async () => { this.process() });
    }
    finally {
      this.stopLoading();
      super.redirect('social-media-connection/youtube')
    }
  }
}
