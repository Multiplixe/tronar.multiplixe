import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class LocalStorageService {

    private sufixKey: string = "FALKOL:";

    setItem(key: string, data: any): void {
        localStorage.setItem(this.keyFactory(key), JSON.stringify(data));
    }

    get<T>(key: string): T {
        return <T>JSON.parse(localStorage.getItem(this.keyFactory(key)));
    }

    has(key: string): boolean {
        const item = this.get<string>(key);
        return item != null;
    }

    clear(): void {
        localStorage.clear();
    }

    delete(key: string): void {
        return localStorage.removeItem(this.keyFactory(key));
    }

    public keyFactory(key: string): string {
        return (this.sufixKey + key).toUpperCase();
    }
}