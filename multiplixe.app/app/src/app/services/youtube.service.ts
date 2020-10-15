import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { SocialMediaProfile } from '../dtos/social-media-profile';

import { youtubeEnvironment } from 'src/environments/environment'
import { rejects } from 'assert';

@Injectable({
  providedIn: 'root'
})

export class YoutubeService extends BaseService {

  private httpService: HttpService;

  constructor() {
    super()

    this.httpService = AppInjector.get(HttpService)

  }

  save(profile: SocialMediaProfile) {
    return this.httpService.postAPI<SocialMediaProfile>("restrito/perfil/youtube", profile);
  }

  oauth2Url() {
    return youtubeEnvironment.oauth2Url;
  }

  async processToken(token: any) {

    return new Promise(async (resolve, rejects) => {


      if (!token.access_token) {
        rejects();
        return;
      }

      let response = await this.getUserInfo(token.access_token);
      let profile = this.parseProfile(response, token.access_token)

      if (profile) {
        this.save(profile)
        resolve(profile);
      }
      else {
        rejects();
      }

    });

  }

  private getUserInfo(token: string) {

    let url = youtubeEnvironment.userInfoUrl + token;

    return this.httpService.get<any>(url, null);

  }

  private parseProfile(user: any, token: string) {

    let profile: SocialMediaProfile = null;

    if (user &&
      user.pageInfo &&
      user.pageInfo.resultsPerPage &&
      user.pageInfo.resultsPerPage == 1) {

      let item = user.items[0];

      profile = new SocialMediaProfile();
      profile.profileId = item.id;
      profile.name = item.snippet.title;
      profile.imageUrl = item.snippet.thumbnails.default.url;
      profile.token = JSON.stringify({ access_token: token });
    }

    return profile;
  }
}
