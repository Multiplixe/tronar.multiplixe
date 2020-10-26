import { LocalStorageService } from './local-storage.service';
import { AppInjector } from '../app-injector';
import { HttpStatusCode } from '../enums/http-status.enum';

export class BaseService {

   protected localStorageService: LocalStorageService;

   constructor() {
      this.localStorageService = AppInjector.get(LocalStorageService);
   }

   getUserId() {
      return this.localStorageService.get<string>("ID");
   }

   async processError(error: any, message: string, callback: Function = null) {

      // if (error && error.status === HttpStatusCode.unauthorized) {
  
      // }
      // else {
      //   this.showMessage(message)
      // }
  
    }   
}