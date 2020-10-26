import { Component, OnInit } from '@angular/core';
import { BasePage } from '../base-page';
import { ActivatedRoute } from '@angular/router';
import { Injector } from "@angular/core";
import { SocialMediaFactory } from '../../services/social-media/factory'
import { ISocialMedia } from 'src/app/services/social-media/social-media.interface';
import { SocialMediaProfileDto } from 'src/app/dtos/social-media-profile.dto';
import { AuthService } from 'src/app/services/auth.service';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { HttpStatusCode } from 'src/app/enums/http-status.enum';

@Component({
  selector: 'app-social-media-connection',
  templateUrl: './social-media-connection.page.html'
})
export class SocialMediaConnectionPage extends BasePage implements OnInit {

  private socialMedia: SocialMediaEnum = SocialMediaEnum.none;
  private route: ActivatedRoute;

  private profiles: SocialMediaProfileDto[] = [];

  public service: ISocialMedia;
  public authService: AuthService;

  constructor(injector: Injector) {
    super();
    this.route = injector.get(ActivatedRoute);
    this.authService = injector.get(AuthService);
  }

  async ngOnInit() {

    try {

      this.route.params.subscribe(async params => {

        this.socialMedia = (SocialMediaEnum[params["socialmedia"]] as unknown) as SocialMediaEnum;

        this.service = SocialMediaFactory(this.socialMedia);

        let response = await this.service.get();

        this.profiles.push(response.item.profile);

        console.log(this.profiles)

        this.canShowContent();
        
      });

    }
    catch (e) {
      if (e.status == HttpStatusCode.notFound) {
        this.canShowContent();
      }
      else {
        this.processError(e, "Ocorreu algum problema ao conectar no servidor. Por favor, tente novamente.");
      }
    }
    finally {
      await this.stopLoading();
    }

  }

  async connect() {
    await this.service.connect();
  }

}
