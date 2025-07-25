using System.Data;
using IMT_Reservas.Server.Shared.Common;

public class CategoriaService : BaseServicios, ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    public CategoriaService(ICategoriaRepository categoriaRepository) => _categoriaRepository = categoriaRepository;
    public virtual void CrearCategoria(CrearCategoriaComando comando)
    {
        try
        {
            ValidarEntradaCreacion(comando);
            _categoriaRepository.Crear(comando);
        }
        catch (ErrorNombreRequerido) { throw; }
        catch (ErrorLongitudInvalida) { throw; }
        catch (Exception ex)
        {
            InterpretarErrorCreacion(comando, ex);
        }
    }
    
    protected override void InterpretarErrorCreacion<T>(T comando, Exception ex)
    {
        if (ex is ErrorDataBase errorDb)
        {
            var mensaje = errorDb.Message?.ToLower() ?? "";
            if (mensaje.Contains("el nombre de la categoría no puede estar vacío")) throw new ErrorNombreRequerido();
            if (errorDb.SqlState == "23505" || mensaje.Contains("ya existe una categoría con el nombre")) throw new ErrorRegistroYaExiste();
            if (mensaje.Contains("error al insertar categoría")) throw new Exception($"Error inesperado al insertar categoría: {errorDb.Message}", errorDb);
            throw new Exception($"Error inesperado de base de datos al crear categoría: {errorDb.Message}", errorDb);
        }
        if (ex is ErrorRepository errorRepo) throw new Exception($"Error del repositorio al crear categoría: {errorRepo.Message}", errorRepo);
        throw ex ?? new Exception("Error desconocido en creación");
    }
    
    protected override void ValidarEntradaCreacion<T>(T comando)
    {
        base.ValidarEntradaCreacion(comando); // Validación base (null check)
        
        // Validaciones específicas para CrearCategoriaComando
        if (comando is CrearCategoriaComando categoriaComando)
        {
            if (string.IsNullOrWhiteSpace(categoriaComando.Nombre)) throw new ErrorNombreRequerido();
            if (categoriaComando.Nombre.Length > 255) throw new ErrorLongitudInvalida("nombre de la categoría", 255);
        }
    }
    public virtual List<CategoriaDto>? ObtenerTodasCategorias()
    {
        try
        {
            DataTable resultado = _categoriaRepository.ObtenerTodos();
            var lista = new List<CategoriaDto>(resultado.Rows.Count);            foreach (DataRow fila in resultado.Rows)
            {
                var baseDto = MapearFilaADto(fila);
                if (baseDto is CategoriaDto categoria)
                    lista.Add(categoria);
            }
            return lista;
        }
        catch { throw; }
    }
    public virtual void ActualizarCategoria(ActualizarCategoriaComando comando)
    {
        try
        {
            ValidarEntradaActualizacion(comando);
            _categoriaRepository.Actualizar(comando);
        }        catch (ErrorIdInvalido) { throw; }
        catch (ErrorNombreRequerido) { throw; }
        catch (ErrorLongitudInvalida) { throw; }
        catch (Exception ex)
        {
            InterpretarErrorActualizacion(comando, ex);
        }
    }
    
    private void InterpretarErrorActualizacion<T>(T comando, Exception ex)
    {
        if (ex is ErrorDataBase errorDb)
        {
            var mensaje = errorDb.Message?.ToLower() ?? "";
            if (mensaje.Contains("no se encontró una categoría activa con id")) throw new ErrorRegistroNoEncontrado();
            if (mensaje.Contains("el nuevo nombre de la categoría no puede estar vacío")) throw new ErrorNombreRequerido();
            if (errorDb.SqlState == "23505" || mensaje.Contains("ya existe otra categoría con el nombre")) throw new ErrorRegistroYaExiste();
            if (mensaje.Contains("error inesperado al actualizar la categoría")) throw new Exception($"Error inesperado al actualizar categoría: {errorDb.Message}", errorDb);
            throw new Exception($"Error inesperado de base de datos al actualizar categoría: {errorDb.Message}", errorDb);
        }
        if (ex is ErrorRepository errorRepo) throw new Exception($"Error del repositorio al actualizar categoría: {errorRepo.Message}", errorRepo);
        throw ex;
    }
    private void ValidarEntradaActualizacion(ActualizarCategoriaComando comando)
    {
        if (comando == null) throw new ArgumentNullException(nameof(comando));
        if (comando.Id <= 0) throw new ErrorIdInvalido("categoría");
        if (string.IsNullOrWhiteSpace(comando.Nombre)) throw new ErrorNombreRequerido();
        if (comando.Nombre.Length > 255) throw new ErrorLongitudInvalida("nombre de la categoría", 255);
    }
    public virtual void EliminarCategoria(EliminarCategoriaComando comando)
    {
        try
        {
            ValidarEntradaEliminacion(comando);
            _categoriaRepository.Eliminar(comando.Id);
        }        catch (ErrorIdInvalido) { throw; }
        catch (Exception ex)
        {
            InterpretarErrorEliminacion(comando, ex);
        }
    }
    
    protected override void InterpretarErrorEliminacion<T>(T comando, Exception ex)
    {
        if (ex is ErrorDataBase errorDb)
        {
            var mensaje = errorDb.Message?.ToLower() ?? "";
            if (mensaje.Contains("no se encontró una categoría activa con id")) throw new ErrorRegistroNoEncontrado();
            if (mensaje.Contains("error al eliminar lógicamente la categoría")) throw new Exception($"Error inesperado al eliminar categoría: {errorDb.Message}", errorDb);
            throw new Exception($"Error inesperado de base de datos al eliminar categoría: {errorDb.Message}", errorDb);
        }
        if (ex is ErrorRepository errorRepo) throw new Exception($"Error del repositorio al eliminar categoría: {errorRepo.Message}", errorRepo);
        throw ex ?? new Exception("Error desconocido en eliminación");
    }
    
    protected override void ValidarEntradaEliminacion<T>(T comando)
    {
        base.ValidarEntradaEliminacion(comando); // Validación base (null check)
        
        // Validaciones específicas para EliminarCategoriaComando
        if (comando is EliminarCategoriaComando categoriaComando)
        {
            if (categoriaComando.Id <= 0) throw new ErrorIdInvalido("categoría");
        }
    }
    protected override BaseDto MapearFilaADto(DataRow fila)
    {
        return new CategoriaDto
        {
            Id = Convert.ToInt32(fila["id_categoria"]),
            Nombre = fila["categoria"] == DBNull.Value ? null : fila["categoria"].ToString(),
        };
    }
}