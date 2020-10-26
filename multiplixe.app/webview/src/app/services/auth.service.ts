import { Injectable } from '@angular/core';

import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  constructor() {
    super()
  }

  getId(): string {
    return this.getUserId();
  }

  getToken(): string {
    return this.localStorageService.get<string>("TOKEN");
  }

  hasToken(): boolean {
    return this.localStorageService.has("TOKEN");
  }

  processToken(token: string) {

    try {
      let split = token.split('.');
      let jwtObject = JSON.parse(atob(split[1]));
      this.localStorageService.setItem("TOKEN", token);
      this.localStorageService.setItem("ID", jwtObject['user_id']);
    }
    catch (e) { 
      console.log("Error", e)
    }
  }

}
