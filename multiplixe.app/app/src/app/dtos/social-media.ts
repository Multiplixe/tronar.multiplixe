export class SocialMedia {
   public name: string = '';
   public ico: string = '';
   public id: number = 0;
   public url: string = '';

   static create() {
      return new SocialMedia();
   }
}