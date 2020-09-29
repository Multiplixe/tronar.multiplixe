import { Injectable } from '@angular/core';

import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import UserProfileEntry from '../dtos/user-profile.entries';
import { UserProfile } from '../dtos/user-profile';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService extends BaseService {

  private httpService: HttpService;

  constructor() {
    super()

    this.httpService = AppInjector.get(HttpService)

  }

  save(user: UserProfileEntry) {
    return this.httpService.postAPI<UserProfileEntry>("usuarios", user)
  }

  update(user: UserProfileEntry) {
    return this.httpService.putAPI<UserProfileEntry>("restrito/usuarios", user)
  }

  get() {
    return this.httpService.getAPI<UserProfile>("restrito/usuarios")
  }  

}
