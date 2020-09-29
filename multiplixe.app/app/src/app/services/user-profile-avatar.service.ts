import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class UserProfileAvatarService extends BaseService {
  
  private httpService: HttpService;

  constructor() {
    super()
    this.httpService = AppInjector.get(HttpService)
  }

  save() {
    return this.httpService.postAPI<any>("restrito/usuarios/avatar", {});
  }

}
