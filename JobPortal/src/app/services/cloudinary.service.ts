import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CloudinaryService {

  constructor(private http: HttpClient) { }

  cloudName = 'dmrcy9oyz';
  uploadPreset = 'ml_default';

  uploadResume(resume: File) {
      const formData = new FormData();
      formData.append("file", resume);
      formData.append("upload_preset", this.uploadPreset)

      return this.http.post(`https://api.cloudinary.com/v1_1/${this.cloudName}/image/upload/`, formData);
  }
}
