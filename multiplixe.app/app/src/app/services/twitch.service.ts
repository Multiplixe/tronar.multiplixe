import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { TwitchAccessTokenDto } from '../dtos/twitch-access-token.dto';

@Injectable({
  providedIn: 'root'
})
export class TwitchService extends BaseService {

  private httpService: HttpService;

  constructor() {
    super();
    this.httpService = AppInjector.get(HttpService)
  }

  async authorize() {
    return this.httpService.getAPI<string>('/restrito/twitch/oauth/authorize');
  }

  async process(parameters:TwitchAccessTokenDto) {
    return this.httpService.getAPI<any>('/restrito/twitch/oauth/process/' + parameters.code);
  }  

}
