//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace projet_2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Client
    {
        public Client()
        {
            this.Commandes = new HashSet<Commande>();
        }
    
        public int NumClient { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        [Display(Name = "Mot de pass")]
        public string MotPasse { get; set; }
        [Required]
        [Compare("MotPasse",ErrorMessage="Le mot de passe n'est pas identique")]
        public string retap_mdp { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        [Phone(ErrorMessage = "The phone number is not valid")]
        [Display(Name = "Num�ro")]
        public string tel { get; set; }
    
        public virtual ICollection<Commande> Commandes { get; set; }
    }
}
