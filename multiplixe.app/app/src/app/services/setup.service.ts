import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { Setup } from '../dtos/setup';
import { UserProfile } from '../dtos/user-profile';
import { DashboardService } from './dashboard.service';
import { Dashboard as DashboardDto } from '../dtos/dashboard';
import { Ranking as RankingDto } from '../dtos/ranking/ranking';
import { RankingService } from './ranking.service';

@Injectable({
  providedIn: 'root'
})
export class SetupService extends BaseService {

  private httpService: HttpService;
  private dashboardService: DashboardService;
  private rankingService: RankingService;

  constructor() {
    super()
    this.httpService = AppInjector.get(HttpService);
    this.dashboardService = AppInjector.get(DashboardService);
    this.rankingService = AppInjector.get(RankingService);
  }

  init() {

    return new Promise(async (resolve, reject) => {
      try {

        let response = await this.get();

        if (response.item) {
          this.userProfile(response.item.userProfile);
          this.dashboard(response.item.dashboard);
          this.ranking(response.item.ranking);
        }

        resolve();
      }
      catch (e) {
        reject(e);
      }
    });

  }

  private get() {
    return this.httpService.getAPI<Setup>("restrito/setup");
  }

  userProfile(userProfile: UserProfile) {
    this.localStorageService.setItem("NICKNAME", userProfile.nickname);
  }

  dashboard(dto: DashboardDto) {
    this.dashboardService.setCache(dto);
  }

  ranking(dto: RankingDto) {
    this.rankingService.setCache(dto);
  }
}
