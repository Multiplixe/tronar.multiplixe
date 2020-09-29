import { RestrictPage } from '../restrict.page';
import { SocialMediaProfileDashboard } from 'src/app/dtos/social-media-profile-dashboard';
import { SocialMediaProfileService } from 'src/app/services/social-media-profile.service';
import { AppInjector } from 'src/app/app-injector';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { HttpStatusCode } from 'src/app/enums/http-status.enum';
import { DataTransferService } from 'src/app/services/data-transfer.service';

export class SocialMediaProfileDashboardBase extends RestrictPage {

   public dashboard: SocialMediaProfileDashboard = SocialMediaProfileDashboard.create();
   public socialMediaProfileService: SocialMediaProfileService;
   private dataTransferService: DataTransferService;

   constructor() {
      super();
      this.socialMediaProfileService = AppInjector.get(SocialMediaProfileService);
   }

   async ngOnInit() {
      super.ngOnInit();
   }

   async load(socialMedia: SocialMediaEnum) {

      try {

         await this.runLoading();

         let response = await this.socialMediaProfileService.getSocialMediaProfile(socialMedia);

         this.dashboard = response.item;

         // futuramente trazer N perfis da mesma rede social;
         this.dashboard.profiles = [];
         this.dashboard.profiles.push(this.dashboard.profile);

         this.canShowContent();
      }
      catch (e) {
         if (e.status == HttpStatusCode.notFound) {
            this.canShowContent();
         }
         else {
            this.processError(e, "Ocorreu algum problema ao conectar no servidor. Por favor, tente novamente.", async () => { this.load(socialMedia) });
         }
      }
      finally {
         await this.stopLoading();
      }

   }

}