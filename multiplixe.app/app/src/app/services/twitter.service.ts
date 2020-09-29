import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { TwitterAccessTokenDto } from '../dtos/twitter-access-token.dto';

@Injectable({
  providedIn: 'root'
})
export class TwitterService extends BaseService {

  private httpService: HttpService;

  constructor() {
    super();
    this.httpService = AppInjector.get(HttpService)
  }

  async authenticate() {
    return this.httpService.getAPI<string>('/restrito/twitter/oauth/authenticate');
  }

  async process(parameters:TwitterAccessTokenDto) {
    return this.httpService.getAPI<any>('/restrito/twitter/oauth/process/' + parameters.token + '/' + parameters.verifier);
  }  

}
