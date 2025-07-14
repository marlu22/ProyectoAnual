namespace Presentation
{
    partial class ConfiguracionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkMayusculasMinusculas = new System.Windows.Forms.CheckBox();
            this.chkNumeros = new System.Windows.Forms.CheckBox();
            this.chkCaracteresEspeciales = new System.Windows.Forms.CheckBox();
            this.chkDobleFactor = new System.Windows.Forms.CheckBox();
            this.chkNoRepetirContrasenas = new System.Windows.Forms.CheckBox();
            this.chkVerificarDatosPersonales = new System.Windows.Forms.CheckBox();
            this.lblMinCaracteres = new System.Windows.Forms.Label();
            this.txtMinCaracteres = new System.Windows.Forms.TextBox();
            this.lblCantPreguntas = new System.Windows.Forms.Label();
            this.txtCantPreguntas = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // chkMayusculasMinusculas
            //
            this.chkMayusculasMinusculas.AutoSize = true;
            this.chkMayusculasMinusculas.Location = new System.Drawing.Point(12, 12);
            this.chkMayusculasMinusculas.Name = "chkMayusculasMinusculas";
            this.chkMayusculasMinusculas.Size = new System.Drawing.Size(209, 19);
            this.chkMayusculasMinusculas.TabIndex = 0;
            this.chkMayusculasMinusculas.Text = "Combinar mayúsculas y minúsculas";
            this.chkMayusculasMinusculas.UseVisualStyleBackColor = true;
            //
            // chkNumeros
            //
            this.chkNumeros.AutoSize = true;
            this.chkNumeros.Location = new System.Drawing.Point(12, 37);
            this.chkNumeros.Name = "chkNumeros";
            this.chkNumeros.Size = new System.Drawing.Size(117, 19);
            this.chkNumeros.TabIndex = 1;
            this.chkNumeros.Text = "Requerir números";
            this.chkNumeros.UseVisualStyleBackColor = true;
            //
            // chkCaracteresEspeciales
            //
            this.chkCaracteresEspeciales.AutoSize = true;
            this.chkCaracteresEspeciales.Location = new System.Drawing.Point(12, 62);
            this.chkCaracteresEspeciales.Name = "chkCaracteresEspeciales";
            this.chkCaracteresEspeciales.Size = new System.Drawing.Size(175, 19);
            this.chkCaracteresEspeciales.TabIndex = 2;
            this.chkCaracteresEspeciales.Text = "Requerir caracteres especiales";
            this.chkCaracteresEspeciales.UseVisualStyleBackColor = true;
            //
            // chkDobleFactor
            //
            this.chkDobleFactor.AutoSize = true;
            this.chkDobleFactor.Location = new System.Drawing.Point(12, 87);
            this.chkDobleFactor.Name = "chkDobleFactor";
            this.chkDobleFactor.Size = new System.Drawing.Size(111, 19);
            this.chkDobleFactor.TabIndex = 3;
            this.chkDobleFactor.Text = "Usar doble factor";
            this.chkDobleFactor.UseVisualStyleBackColor = true;
            //
            // chkNoRepetirContrasenas
            //
            this.chkNoRepetirContrasenas.AutoSize = true;
            this.chkNoRepetirContrasenas.Location = new System.Drawing.Point(12, 112);
            this.chkNoRepetirContrasenas.Name = "chkNoRepetirContrasenas";
            this.chkNoRepetirContrasenas.Size = new System.Drawing.Size(194, 19);
            this.chkNoRepetirContrasenas.TabIndex = 4;
            this.chkNoRepetirContrasenas.Text = "No repetir contraseñas anteriores";
            this.chkNoRepetirContrasenas.UseVisualStyleBackColor = true;
            //
            // chkVerificarDatosPersonales
            //
            this.chkVerificarDatosPersonales.AutoSize = true;
            this.chkVerificarDatosPersonales.Location = new System.Drawing.Point(12, 137);
            this.chkVerificarDatosPersonales.Name = "chkVerificarDatosPersonales";
            this.chkVerificarDatosPersonales.Size = new System.Drawing.Size(157, 19);
            this.chkVerificarDatosPersonales.TabIndex = 5;
            this.chkVerificarDatosPersonales.Text = "Verificar datos personales";
            this.chkVerificarDatosPersonales.UseVisualStyleBackColor = true;
            //
            // lblMinCaracteres
            //
            this.lblMinCaracteres.AutoSize = true;
            this.lblMinCaracteres.Location = new System.Drawing.Point(12, 165);
            this.lblMinCaracteres.Name = "lblMinCaracteres";
            this.lblMinCaracteres.Size = new System.Drawing.Size(129, 15);
            this.lblMinCaracteres.TabIndex = 6;
            this.lblMinCaracteres.Text = "Mínimo de caracteres:";
            //
            // txtMinCaracteres
            //
            this.txtMinCaracteres.Location = new System.Drawing.Point(147, 162);
            this.txtMinCaracteres.Name = "txtMinCaracteres";
            this.txtMinCaracteres.Size = new System.Drawing.Size(100, 23);
            this.txtMinCaracteres.TabIndex = 7;
            //
            // lblCantPreguntas
            //
            this.lblCantPreguntas.AutoSize = true;
            this.lblCantPreguntas.Location = new System.Drawing.Point(12, 194);
            this.lblCantPreguntas.Name = "lblCantPreguntas";
            this.lblCantPreguntas.Size = new System.Drawing.Size(132, 15);
            this.lblCantPreguntas.TabIndex = 8;
            this.lblCantPreguntas.Text = "Cantidad de preguntas:";
            //
            // txtCantPreguntas
            //
            this.txtCantPreguntas.Location = new System.Drawing.Point(147, 191);
            this.txtCantPreguntas.Name = "txtCantPreguntas";
            this.txtCantPreguntas.Size = new System.Drawing.Size(100, 23);
            this.txtCantPreguntas.TabIndex = 9;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(172, 220);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            //
            // ConfiguracionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 256);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtCantPreguntas);
            this.Controls.Add(this.lblCantPreguntas);
            this.Controls.Add(this.txtMinCaracteres);
            this.Controls.Add(this.lblMinCaracteres);
            this.Controls.Add(this.chkVerificarDatosPersonales);
            this.Controls.Add(this.chkNoRepetirContrasenas);
            this.Controls.Add(this.chkDobleFactor);
            this.Controls.Add(this.chkCaracteresEspeciales);
            this.Controls.Add(this.chkNumeros);
            this.Controls.Add(this.chkMayusculasMinusculas);
            this.Name = "ConfiguracionForm";
            this.Text = "Configuración de Seguridad";
            this.Load += new System.EventHandler(this.ConfiguracionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMayusculasMinusculas;
        private System.Windows.Forms.CheckBox chkNumeros;
        private System.Windows.Forms.CheckBox chkCaracteresEspeciales;
        private System.Windows.Forms.CheckBox chkDobleFactor;
        private System.Windows.Forms.CheckBox chkNoRepetirContrasenas;
        private System.Windows.Forms.CheckBox chkVerificarDatosPersonales;
        private System.Windows.Forms.Label lblMinCaracteres;
        private System.Windows.Forms.TextBox txtMinCaracteres;
        private System.Windows.Forms.Label lblCantPreguntas;
        private System.Windows.Forms.TextBox txtCantPreguntas;
        private System.Windows.Forms.Button btnGuardar;
    }
}
