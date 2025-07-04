import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsuarioService } from '../../../services/usuario/usuario.service';
import { Usuario } from '../../../models/usuario';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioServiceAPI } from '../../../services/APIS/Usuario/usuario.service';
import { CarreraService } from '../../../services/APIS/Carrera/carrera.service';

@Component({
  selector: 'app-registrar-usuario',
  imports: [FormsModule, CommonModule],
  templateUrl: './registrar-usuario.component.html',
  styleUrl: './registrar-usuario.component.css'
})
export class RegistrarUsuarioComponent {
  nuevoUsuario: Usuario = new Usuario();
  password: string = '';
  confirmPassword: string = '';
  carreras: string[] = []; 
  constructor(private usuarioS: UsuarioService, private router: Router , private registrarcuenta : UsuarioServiceAPI , private carrerasS : CarreraService) {}


  ngOnInit() {
    this.carrerasS.obtenerCarreras().subscribe({
      next: (response) => {
         this.carreras = response.map(carrera => carrera.nombre); 
      },
      error: (error) => {
        console.error('Error al obtener las carreras:', error.error.mensaje);
      }

    });

  }





  registrar() {
    this.nuevoUsuario.rol = 'usuario';
    
    this.registrarcuenta.registrarCuenta(this.nuevoUsuario,this.password, "estudiante").subscribe(
      (response) => {
        console.log('Usuario registrado exitosamente:', response);
        this.usuarioS.usuario = this.nuevoUsuario;
      },
      (error) => {
        alert('Error al registrar el usuario:' + error.error.mensaje);
      }
    );



    this.router.navigate(['/home']);
  }

  irALogin() {
    this.router.navigate(['/Iniciar-Sesion']);
  }
  validartelefono(telefono : string ) : boolean{
    const regex = /^[-+0-9]+$/;
   return !regex.test(telefono);
  }
}
