import { Injectable } from '@angular/core';

import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import Login from '../dtos/login';
import { PasswordResetRequestEntries } from '../dtos/password-reset-request.entries';
import { PasswordResetEntries } from '../dtos/password-reset.entries';

import { AngularFireAuth } from '@angular/fire/auth';
import { TokenAuth } from '../dtos/token-auth';
import { HttpStatusCode } from '../enums/http-status.enum';
import { firebaseEnvironment } from '../../environments/environment';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  private httpService: HttpService;
  private angularFireAuth: AngularFireAuth;

  constructor() {
    super()
    this.httpService = AppInjector.get(HttpService);
    this.angularFireAuth = AppInjector.get(AngularFireAuth);
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

  logoff() {

    return new Promise((resolve, reject) => {
      this.angularFireAuth.signOut()
        .then(() => {
          this.localStorageService.clear();
          resolve()
        })
        .catch(() => {
          reject();
        });
    });

  }

  async authenticate(login: Login): Promise<any> {

    return new Promise(async (resolve, reject) => {

      try {

        this.angularFireAuth.setPersistence('local')
          .then(async () => {

            await this.angularFireAuth.signInWithEmailAndPassword(login.email, login.password)

            this.angularFireAuth.onAuthStateChanged(async (u) => {

              if (u) {

                var token = await u.getIdToken(true);

                let tokenAuth: TokenAuth = new TokenAuth(token, u.refreshToken);

                this.processToken(tokenAuth);
              }
              
              resolve(u);

            }).catch((e) => {
              reject(e)
            });

          })
          .catch((e) => {
            reject(e)
          });

      }
      catch (e) {
        reject(e)
      }

    });

  }

  async processToken(tokenAuth: TokenAuth) {

    let split = tokenAuth.token.split('.');
    let jwtObject = JSON.parse(atob(split[1]));
    this.localStorageService.setItem("TOKEN", tokenAuth.token);
    this.localStorageService.setItem("REFRESH-TOKEN", tokenAuth.refresh);
    this.localStorageService.setItem("ID", jwtObject['user_id']);
  }

  async passwordResetRequest(request: PasswordResetRequestEntries) {
    return this.angularFireAuth.sendPasswordResetEmail(request.email.value);
  }

  async passwordReset(request: PasswordResetEntries) {
    return this.angularFireAuth.confirmPasswordReset(request.oobCode, request.password.value);
  }

  async passwordResetValidate(reset: PasswordResetEntries) {
    return this.angularFireAuth.verifyPasswordResetCode(reset.oobCode);
  }

  async isAuthenticated() {

    return new Promise((resolve, reject) => {

      this.httpService.getAPI<any>("restrito/auth")
        .then(() => {
          resolve();
        })
        .catch(async (e) => {

          if (e.status == HttpStatusCode.unauthorized && this.localStorageService.has("REFRESH-TOKEN")) {
            this.refreshTokenProcess()
              .then(() => resolve());
            return;
          }

          reject(e);

        });
    });
  }

  async refreshTokenProcess() {

    return new Promise(async (resolve, reject) => {

      try {

        let url = firebaseEnvironment.refreshTokenURL + firebaseEnvironment.apiKey;

        let refreshToken = this.localStorageService.get<string>("REFRESH-TOKEN");

        let body = {
          "grant_type": "refresh_token",
          "refresh_token": refreshToken
        }

        let headers: HttpHeaders = new HttpHeaders();
        headers.set("Content-Type", "application/x-www-form-urlencoded");

        let response = await this.httpService.post<any>(url, body, headers);

        let tokenAuth: TokenAuth = new TokenAuth(response.access_token, response.refresh_token);

        this.processToken(tokenAuth);

        resolve();

      }
      catch (e) {
        reject(e);
      }

    });

  }
}
