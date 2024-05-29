using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AgenciaViajes.Models;

public partial class AgenciaVContext : DbContext
{
    public AgenciaVContext()
    {
    }

    public AgenciaVContext(DbContextOptions<AgenciaVContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aerolinea> Aerolineas { get; set; }

    public virtual DbSet<Aeropuert> Aeropuerts { get; set; }

    public virtual DbSet<Asiento> Asientos { get; set; }

    public virtual DbSet<Credenciale> Credenciales { get; set; }

    public virtual DbSet<Detallecompra> Detallecompras { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<RegistroVuelo> RegistroVuelos { get; set; }

    public virtual DbSet<ReservaHotel> ReservaHotels { get; set; }

    public virtual DbSet<Thabitacion> Thabitacions { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vendedor> Vendedors { get; set; }

    public virtual DbSet<Vuelo> Vuelos { get; set; }

   /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-SJ3DLU9H; database=AgenciaV; Trusted_Connection=True; TrustServerCertificate=True; Encrypt=false;");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.IdAerolinea).HasName("PK__Aeroline__6397B34CDF4055A7");

            entity.ToTable("Aerolinea");

            entity.Property(e => e.IdAerolinea).HasColumnName("ID_Aerolinea");
            entity.Property(e => e.Aerolinea1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerolinea");
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Aerolineas)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Aerolinea__Estad__44FF419A");
        });

        modelBuilder.Entity<Aeropuert>(entity =>
        {
            entity.HasKey(e => e.IdAeropuerto).HasName("PK__Aeropuer__74D09F6A85565878");

            entity.ToTable("Aeropuert");

            entity.Property(e => e.IdAeropuerto).HasColumnName("Id_aeropuerto");
            entity.Property(e => e.Aeropuerto)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Provincia)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Aeropuerts)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Aeropuert__Estad__4222D4EF");
        });

        modelBuilder.Entity<Asiento>(entity =>
        {
            entity.HasKey(e => e.IdAsiento).HasName("PK__ASIENTO__6838AB9CB39FA6DC");

            entity.ToTable("ASIENTO");

            entity.Property(e => e.IdAsiento).HasColumnName("ID_asiento");
            entity.Property(e => e.Clase)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Disponibilidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("disponibilidad");
            entity.Property(e => e.IdVuelo).HasColumnName("ID_vuelo");
            entity.Property(e => e.NumAsiento)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Num_Asiento");
            entity.Property(e => e.PrecioV)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio_v");

            entity.HasOne(d => d.IdVueloNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdVuelo)
                .HasConstraintName("FK__ASIENTO__ID_vuel__4D94879B");
        });

        modelBuilder.Entity<Credenciale>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Detallecompra>(entity =>
        {
            entity.HasKey(e => e.NumeroVenta).HasName("PK__Detallec__55FC5A903B5DBA6E");

            entity.ToTable("Detallecompra");

            entity.Property(e => e.NumeroVenta).HasColumnName("Numero_venta");
            entity.Property(e => e.Cvv)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CVV");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_compra");
            entity.Property(e => e.IdPrh).HasColumnName("ID_PRH");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.IdRvuelo).HasColumnName("ID_Rvuelo");
            entity.Property(e => e.TVencimiento)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("T_vencimiento");
            entity.Property(e => e.TarjetaCredito)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("tarjeta_credito");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");

            entity.HasOne(d => d.DniNavigation).WithMany(p => p.Detallecompras)
                .HasForeignKey(d => d.Dni)
                .HasConstraintName("FK__Detallecomp__DNI__619B8048");

            entity.HasOne(d => d.IdPrhNavigation).WithMany(p => p.Detallecompras)
                .HasForeignKey(d => d.IdPrh)
                .HasConstraintName("FK__Detalleco__ID_PR__628FA481");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Detallecompras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Detalleco__ID_Pr__6477ECF3");

            entity.HasOne(d => d.IdRvueloNavigation).WithMany(p => p.Detallecompras)
                .HasForeignKey(d => d.IdRvuelo)
                .HasConstraintName("FK__Detalleco__ID_Rv__6383C8BA");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Estado1).HasName("PK__Estado__36DF552E17172A7B");

            entity.ToTable("Estado");

            entity.Property(e => e.Estado1)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("Estado");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.IdHotel).HasName("PK__Hotel__198505EFA794D5C6");

            entity.ToTable("Hotel");

            entity.Property(e => e.IdHotel).HasColumnName("ID_Hotel");
            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Provincia)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Hotel__Estado__5629CD9C");
        });

        modelBuilder.Entity<RegistroVuelo>(entity =>
        {
            entity.HasKey(e => e.IdRvuelo).HasName("PK__Registro__C1DFD10704A9863C");

            entity.ToTable("RegistroVuelo");

            entity.Property(e => e.IdRvuelo).HasColumnName("ID_Rvuelo");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.IdAsiento).HasColumnName("ID_asiento");
            entity.Property(e => e.IdVuelo).HasColumnName("ID_vuelo");
            entity.Property(e => e.PrecioT)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Precio_t");

            entity.HasOne(d => d.DniNavigation).WithMany(p => p.RegistroVuelos)
                .HasForeignKey(d => d.Dni)
                .HasConstraintName("FK__RegistroVue__DNI__534D60F1");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.RegistroVuelos)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__RegistroV__Estad__5070F446");

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.RegistroVuelos)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK__RegistroV__ID_as__5165187F");

            entity.HasOne(d => d.IdVueloNavigation).WithMany(p => p.RegistroVuelos)
                .HasForeignKey(d => d.IdVuelo)
                .HasConstraintName("FK__RegistroV__ID_vu__52593CB8");
        });

        modelBuilder.Entity<ReservaHotel>(entity =>
        {
            entity.HasKey(e => e.IdPrh).HasName("PK__ReservaH__20AF98F4BFE99A75");

            entity.ToTable("ReservaHotel");

            entity.Property(e => e.IdPrh).HasColumnName("ID_PRH");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.FechaEntrada)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_entrada");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_salida");
            entity.Property(e => e.IdHotel).HasColumnName("ID_Hotel");
            entity.Property(e => e.IdProductoTh).HasColumnName("ID_ProductoTH");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.DniNavigation).WithMany(p => p.ReservaHotels)
                .HasForeignKey(d => d.Dni)
                .HasConstraintName("FK__ReservaHo__Estad__5BE2A6F2");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.ReservaHotels)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__ReservaHo__Estad__5CD6CB2B");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.ReservaHotels)
                .HasForeignKey(d => d.IdHotel)
                .HasConstraintName("FK__ReservaHo__ID_Ho__5DCAEF64");

            entity.HasOne(d => d.IdProductoThNavigation).WithMany(p => p.ReservaHotels)
                .HasForeignKey(d => d.IdProductoTh)
                .HasConstraintName("FK__ReservaHo__ID_Pr__5EBF139D");
        });

        modelBuilder.Entity<Thabitacion>(entity =>
        {
            entity.HasKey(e => e.IdProductoTh).HasName("PK__THabitac__61E0CD25BA641F22");

            entity.ToTable("THabitacion");

            entity.Property(e => e.IdProductoTh).HasColumnName("ID_ProductoTH");
            entity.Property(e => e.CantidadPersonas).HasColumnName("Cantidad_Personas");
            entity.Property(e => e.Disponibilidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("disponibilidad");
            entity.Property(e => e.Habitacion)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.IdHotel).HasColumnName("ID_Hotel");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Thabitacions)
                .HasForeignKey(d => d.IdHotel)
                .HasConstraintName("FK__THabitaci__ID_Ho__59063A47");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Tour__9B4120E2A7518592");

            entity.ToTable("Tour");

            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.Descripción)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Destinos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_inicio");
            entity.Property(e => e.NombreTour)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Tour");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Tour__Estado__3F466844");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PK__Usuario__C035B8DC9742BCDF");

            entity.ToTable("Usuario");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha_nacimiento");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("numero");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Usuario__Estado__398D8EEE");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.Usuario).HasName("PK__Vendedor__E3237CF6570C0294");

            entity.ToTable("Vendedor");

            entity.Property(e => e.Usuario)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Vendedors)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Vendedor__Estado__3C69FB99");
        });

        modelBuilder.Entity<Vuelo>(entity =>
        {
            entity.HasKey(e => e.IdVuelo).HasName("PK__Vuelo__4D9069A9C415820D");

            entity.ToTable("Vuelo");

            entity.Property(e => e.IdVuelo).HasColumnName("ID_vuelo");
            entity.Property(e => e.Estado)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.FechaFinal)
                .HasColumnType("datetime")
                .HasColumnName("fecha_final");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdAerolinea).HasColumnName("ID_Aerolinea");
            entity.Property(e => e.IdAeropuertoDestino).HasColumnName("Id_aeropuerto_destino");
            entity.Property(e => e.IdAeropuertoOrigen).HasColumnName("Id_aeropuerto_origen");
            entity.Property(e => e.PrecioV)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio_v");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Vuelos)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Vuelo__Estado__47DBAE45");

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.Vuelos)
                .HasForeignKey(d => d.IdAerolinea)
                .HasConstraintName("FK__Vuelo__ID_Aeroli__4AB81AF0");

            entity.HasOne(d => d.IdAeropuertoDestinoNavigation).WithMany(p => p.VueloIdAeropuertoDestinoNavigations)
                .HasForeignKey(d => d.IdAeropuertoDestino)
                .HasConstraintName("FK__Vuelo__Id_aeropu__49C3F6B7");

            entity.HasOne(d => d.IdAeropuertoOrigenNavigation).WithMany(p => p.VueloIdAeropuertoOrigenNavigations)
                .HasForeignKey(d => d.IdAeropuertoOrigen)
                .HasConstraintName("FK__Vuelo__Id_aeropu__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
