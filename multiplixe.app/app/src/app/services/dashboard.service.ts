import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { Dashboard as DashboardDto } from '../dtos/dashboard';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardService extends BaseService {

  private httpService: HttpService;
  private cacheService: CacheService;

  constructor() {
    super()
    this.httpService = AppInjector.get(HttpService);
    this.cacheService = AppInjector.get(CacheService);
  }

  async get() {
    return this.cacheService.getDashboard();
  }  

  async sincronize() {

    return new Promise(async (resolve, reject) => {

      try {

        let response = await this.httpService.getAPI<DashboardDto>("restrito/dashboard/");

        this.setCache(response.item);

        resolve();

      }
      catch (e) {
        reject(e);
      }
    });
  }

  setCache(dashboard: DashboardDto) {
    this.cacheService.setDashboard(dashboard);
  }
}
