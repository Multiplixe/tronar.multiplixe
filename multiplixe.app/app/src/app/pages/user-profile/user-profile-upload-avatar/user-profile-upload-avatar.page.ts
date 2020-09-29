import { Component, OnInit } from '@angular/core';
import { AppInjector } from 'src/app/app-injector';
import { UploadService } from 'src/app/services/upload.service';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { BasePage } from '../../base.page';
import { DisplayControl } from 'src/app/dtos/display-control';
import { RestrictPage } from '../../restrict.page';

@Component({
  selector: 'app-user-profile-upload-avatar',
  templateUrl: './user-profile-upload-avatar.page.html'
})

export class UserProfileUploadAvatarPage extends RestrictPage implements OnInit {

  public files: File[] = []
  public loader: boolean = false;
  public displayControl: DisplayControl = DisplayControl.create();

  public maxSize: number = 4096000;
  public validComboDrag: any;

  public invalidClear: boolean = false;

  private uploadService: UploadService;

  public imageBase64: any = '';

  public croppedImage: any = '';

  public error: any;

  constructor() {
    super();
    this.uploadService = AppInjector.get(UploadService);

  }

  async selectImage() {

    try {
      const reader = new FileReader();
      reader.readAsDataURL(this.files[0]);
      reader.onload = () => {
        this.imageBase64 = reader.result;
        this.displayControl.set(1);
      };

    }
    catch (e) {
      console.log("E", e);
    }
  }

  async upload() {

    await this.runLoading();

    fetch(this.croppedImage)
      .then(b => b.blob())
      .then(async (blob) => {

        let formData = new FormData();
        formData.append('image', blob, this.files[0].name);

        try {
          await this.uploadService.avatar(formData)

          this.redirectRestrict('perfil');
        }
        catch (e) {
          this.processError(e, "Ocorreu algum problema ao conectar no servidor. Por favor, tente novamente.", async () => { this.upload() });
        }
        finally {
          await this.stopLoading();
        }

      })
  }

  cancel() {
    this.files = [];
    this.displayControl.set(0);
  }

  disabledButton() {
    return this.files.length == 0;
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }

  imageLoaded() { }
  cropperReady() { }
  loadImageFailed() { }

  async ngOnInit() {
    super.ngOnInit();
    this.displayControl.set(0);
  }
}
