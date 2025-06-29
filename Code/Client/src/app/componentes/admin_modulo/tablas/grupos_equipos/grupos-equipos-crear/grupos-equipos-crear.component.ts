import { Component, EventEmitter, Input, Output, signal, WritableSignal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GrupoEquipo } from '../../../../../models/grupo_equipo';
import { GrupoequipoService } from '../../../../../services/APIS/GrupoEquipo/grupoequipo.service';

@Component({
  selector: 'app-grupos-equipos-crear',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './grupos-equipos-crear.component.html',
  styleUrl: './grupos-equipos-crear.component.css'
})
export class GruposEquiposCrearComponent {

  @Input() botoncrear: WritableSignal<boolean> = signal(true);
  @Input() categorias: string[] = [];
  @Output() Actualizar = new EventEmitter<void>();

  grupoEquipo: GrupoEquipo = new GrupoEquipo();

  constructor(private grupoEquipoapi: GrupoequipoService) { }

  registrar() {
    this.grupoEquipoapi.crearGrupoEquipo(this.grupoEquipo).subscribe({
      next: (response) => {
        this.Actualizar.emit();
        this.cerrar();
      },
      error: (error) => {
        alert(error.error.error + ': ' + error.error.mensaje);
      }
    });
  }

  cerrar() {
    this.botoncrear.set(false);
  }
}
