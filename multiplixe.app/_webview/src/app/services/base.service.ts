import { LocalStorageService } from './local-storage.service';
import { AppInjector } from '../app-injector';

export class BaseService {

   protected localStorageService: LocalStorageService;

   constructor() {
      this.localStorageService = AppInjector.get(LocalStorageService);
   }

   getUserId() {
      return this.localStorageService.get<string>("ID");
   }
}