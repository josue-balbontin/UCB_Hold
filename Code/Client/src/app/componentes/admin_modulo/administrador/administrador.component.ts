import { Component, createNgModuleRef } from '@angular/core';
import {Router} from '@angular/router';
import { SidebardComponent } from '../../sidebard/sidebard.component';
import { AccesoriosTablaComponent } from '../tablas/accesorios/accesorios-tabla/accesorios-tabla.component';
import { CarrerasTablaComponent } from '../tablas/carreras/carreras-tabla/carreras-tabla.component';
import { CategoriasTablaComponent } from '../tablas/categorias/categorias-tabla/categorias-tabla.component';
import { UsuariosTablaComponent } from '../tablas/usuarios/usuarios-tabla/usuarios-tabla.component';
import { ComponentesTablaComponent } from '../tablas/componentes/componentes-tabla/componentes-tabla.component';
import { EmpresasMantenimientoTablaComponent } from '../tablas/empresas_mantenimiento/empresas-mantenimiento-tabla/empresas-mantenimiento-tabla.component';
import { EquiposTablaComponent } from '../tablas/equipos/equipos-tabla/equipos-tabla.component';
import { GaveterosTablaComponent } from '../tablas/gaveteros/gaveteros-tabla/gaveteros-tabla.component';
import { GruposEquiposTablaComponent } from '../tablas/grupos_equipos/grupos-equipos-tabla/grupos-equipos-tabla.component';
import { MantenimientosTablaComponent } from '../tablas/mantenimientos/mantenimientos-tabla/mantenimientos-tabla.component';
import { MueblesTablaComponent } from '../tablas/muebles/muebles-tabla/muebles-tabla.component';
import { PrestamosTablaComponent } from '../tablas/prestamos/prestamos-tabla/prestamos-tabla.component';
import { UsuarioService } from '../../../services/usuario/usuario.service';

@Component({
  selector: 'app-administrador',
  standalone: true,
  imports: [SidebardComponent,AccesoriosTablaComponent,CarrerasTablaComponent,UsuariosTablaComponent,
    CategoriasTablaComponent,ComponentesTablaComponent,EmpresasMantenimientoTablaComponent,EquiposTablaComponent,GaveterosTablaComponent,
    GruposEquiposTablaComponent,MantenimientosTablaComponent,MueblesTablaComponent,PrestamosTablaComponent],
  templateUrl: './administrador.component.html',
  styleUrls: ['./administrador.component.css']
})

export class AdministradorComponent {
  tablas : string[] = ['Prestamos', 'Carreras', 'Usuarios', 'Categorias', 'Componentes', "Empresas de Mantenimiento",
     'Equipos', 'Gaveteros', 'Grupos de Equipos', 'Mantenimientos', 'Muebles',
      'Accesorios'];
  
  item : string = 'Prestamos';

  constructor(public  router: Router , private usuario :UsuarioService) {
  }

  ngOnInit() {
    if(this.usuario.vacio()){
      this.router.navigate(['/Iniciar-Sesion']);
    }
    else if(this.usuario.obtenerrol()!="administrador"){
      this.router.navigate(['/home']);
    }
  }

  clickitem(item : string){
    this.item = item;
  }



}
