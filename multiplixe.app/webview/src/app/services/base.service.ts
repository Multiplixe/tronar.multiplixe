import { LocalStorageService } from './local-storage.service';
import { AppInjector } from '../app-injector';
import { HttpStatusCode } from '../enums/http-status.enum';
import { AuthService } from '../services/auth.service';

export class BaseService {

   public localStorageService: LocalStorageService;
   public authService: AuthService;

   constructor() {
      this.localStorageService = AppInjector.get(LocalStorageService);
      this.authService = new AuthService();
   }

   getUserId() {
      return this.localStorageService.get<string>("ID");
   }

  async isAuthenticated() {
     return this.authService.isAuthenticated();
  }

   async processError(error: any, message: string, successCallback: Function = null) {

      if (error && error.status === HttpStatusCode.unauthorized) {

         await this.authService.renewAccessToken();

         if (successCallback) {
            successCallback();
         }

      }
   }
}