import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage-angular';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  private _storage: Storage | null = null;

  constructor(private storage: Storage) {
    this.init();
    console.log('STORAGE INIT...');
  }

  async init() {
    const storage = await this.storage.create();
    this._storage = storage;
  }

  public async set(key: string, value: any): Promise<any> {
    try {
      const result = await this._storage?.set(key, value);
      return result;
    } catch {
      return {};
    }
  }

  public async get(key: string): Promise<any> {
    try {
      const result = await this._storage?.get(key);
      //console.log('GET', key, result);
      return result;
    } catch {
      return {};
    }
  }

  public async getToken(): Promise<string> {
    const key: string = 'token';
    const token: string = await this.get(key);
    return token;
  }

  public async setToken(token: string): Promise<any> {
    const key: string = 'token';
    return await this.set(key, token);
  }

  public async remove(key: string): Promise<boolean> {
    try {
      await this._storage?.remove(key);
      return true;
    } catch {
      return false;
    }
  }
}
