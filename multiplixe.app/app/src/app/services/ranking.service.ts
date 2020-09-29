import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { Ranking } from '../dtos/ranking/ranking';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class RankingService extends BaseService {

  private httpService: HttpService;
  private cacheService: CacheService;

  constructor() {
    super();
    this.httpService = AppInjector.get(HttpService);
    this.cacheService = AppInjector.get(CacheService);
  }

  private async cacheControl() {
    let updated = this.cacheService.getUpdatedRanking();
    if(!updated) {
      await this.sincronize();
    }
  }

  async get() {
    await this.cacheControl();
    return this.cacheService.getRanking();
  }

  async getUserRanking() {
    await this.cacheControl();
    return this.cacheService.getUserRanking();
  }

  async sincronize() {

    return new Promise(async (resolve, reject) => {

      try {

        let response = await this.httpService.getAPI<Ranking>("restrito/ranking");

        this.setCache(response.item);

        resolve();
      }
      catch (e) {
        reject(e);
      }
    });
  }

  setCache(ranking: Ranking) {
    this.cacheService.setRanking(ranking);

    let id = super.getUserId();

    let placingCurrentUser = ranking.placing.find(f => f.user == id);

    let userRanking = 0;

    if (placingCurrentUser) {
      userRanking = placingCurrentUser.value;
    }

    this.cacheService.setUserRanking(userRanking);
  }

}
