import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FavoritosService {
  apiUrl = environment.apiUrl+"/api";

  constructor(private http : HttpClient) { }

  obtenerFavoritos(carnet: string) {
    return this.http.get(`${this.apiUrl}/favoritos/${carnet}`);
  }

  obtenerSiFavorito(carnet: string, id_grupo_equipo: number) {
    return this.http.get(`${this.apiUrl}/favoritos/${carnet}/${id_grupo_equipo}`).pipe(
      map((response: any) => {
        return response.exists;
      })
    );
  }

  agregarFavorito(carnet: string, id_grupo_equipo: number) {
    
    const envio={
      carnet: carnet,
      id_grupo_equipo: id_grupo_equipo
    }
    return this.http.post(`${this.apiUrl}/agregar_favorito`, envio);
  }


  eliminarFavorito(carnet: string, id_grupo_equipo: number) {
    return this.http.delete(`${this.apiUrl}/favoritos/${carnet}/${id_grupo_equipo}`);

  }

 


 



}
