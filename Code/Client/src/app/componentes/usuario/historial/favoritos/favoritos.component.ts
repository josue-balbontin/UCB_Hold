import { Component } from '@angular/core';
import { FavoritosService } from '../../../../services/APIS/Favorito/favoritos.service';
import { UsuarioService } from '../../../../services/usuario/usuario.service';

@Component({
  selector: 'app-favoritos',
  imports: [],
  templateUrl: './favoritos.component.html',
  styleUrl: './favoritos.component.css'
})
export class FavoritosComponent {
  favoritos: any[] = []; 

  //carnet
  //id_grupo_equipo

  constructor(private favoritosAPI : FavoritosService ,public usuario : UsuarioService){
    
  }

  ngOnInit() {

    if(this.usuario.vacio()){

    }
    else{
    this.obtenerfavoritos();
    }
 
  }

  obtenerfavoritos() {
    this.favoritosAPI.obtenerFavoritos(this.usuario.obtenercarnet()).subscribe(
      (response: any) => {
        this.favoritos = response;
      },
      (error: any) => {
        console.error('Error al obtener los favoritos:', error);
      }
    );
  }


}
