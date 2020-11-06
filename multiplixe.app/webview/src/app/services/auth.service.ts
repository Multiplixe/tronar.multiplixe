import { Injectable } from '@angular/core';

import { firebaseEnvironment } from '../../environments/environment';
import { HttpHeaders } from '@angular/common/http';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { LocalStorageService } from './local-storage.service';
import { HttpStatusCode } from '../enums/http-status.enum';
import { PasswordResetEntries } from '../dtos/password-reset.entries';

import { AngularFireAuth } from '@angular/fire/auth';
import { promise } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private httpService: HttpService;
  private localStorageService: LocalStorageService;
  private angularFireAuth: AngularFireAuth;

  constructor() {
    this.httpService = AppInjector.get(HttpService);
    this.localStorageService = AppInjector.get(LocalStorageService);
    this.angularFireAuth = AppInjector.get(AngularFireAuth);
  }

  getId(): string {
    return this.localStorageService.get<string>("ID");
  }

  getAccessToken(): string {
    return this.localStorageService.get<string>("ACCESS-TOKEN");
  }

  hasAcessToken(): boolean {
    return this.localStorageService.has("ACCESS-TOKEN");
  }

  async isAuthenticated() {

    return new Promise((resolve, reject) => {

      this.httpService.getAPI<any>("restrito/auth")
        .then(() => {
          resolve();
        })
        .catch(async (e) => {

          if (e.status == HttpStatusCode.unauthorized && this.localStorageService.has("REFRESH-TOKEN")) {
            this.renewAccessToken()
              .then(() => resolve())
              .catch(() => reject());
            return;
          }

          reject(e);

        });
    });
  }

  processAccessToken(access_token: string) {
    return new Promise(async (resolve, reject) => {
      try {
        this.localStorageService.setItem("ACCESS-TOKEN", access_token);
        resolve();
      }
      catch (e) {
        reject(e);
      }
    });
  }

  processRefreshToken(refresh_token: string) {

    return new Promise(async (resolve, reject) => {

      try {

        this.localStorageService.setItem("REFRESH-TOKEN", refresh_token);

        await this.renewAccessToken();

        resolve();

      }
      catch (e) {
        reject(e);
      }

    });

  }

  renewAccessToken() {

    return new Promise(async (resolve, reject) => {

      try {

        let refresh_token = this.localStorageService.get<string>("REFRESH-TOKEN");

        let url = firebaseEnvironment.refreshTokenURL + firebaseEnvironment.apiKey;

        let body = {
          "grant_type": "refresh_token",
          "refresh_token": refresh_token
        }

        let headers: HttpHeaders = new HttpHeaders();
        headers.set("Content-Type", "application/x-www-form-urlencoded");

        let response = await this.httpService.post<any>(url, body, headers);

        this.localStorageService.setItem("ACCESS-TOKEN", response.access_token);
        this.localStorageService.setItem("REFRESH-TOKEN", response.refresh_token);
        this.localStorageService.setItem("ID", response.user_id);
        resolve();

      }
      catch (e) {
        console.log("e", e)
        reject(e);
      }

    });

  }

  async passwordReset(request: PasswordResetEntries) {
    return this.angularFireAuth.confirmPasswordReset(request.oobCode, request.password.value);
  }

  async passwordResetValidate(reset: PasswordResetEntries) {
    return this.angularFireAuth.verifyPasswordResetCode(reset.oobCode);
  }

}
