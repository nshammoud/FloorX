using KQF.Floor.Web.NavServices.Lookup;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;

namespace KQF.Floor.Web.Models
{
    public class MoveMenuModel
    {
        public MoveMenuModel(string bin, string ctr, string itm, string lot, decimal? qty)
        {
            Bin = bin;
            Container = ctr;
            Item = itm;
            Lot = lot;
            Quantity = qty;
               
        }

        public string Bin { get; set; }

        public string Container { get; set; }

        public string Item { get; set; }

        public string Lot { get; set; }

        public decimal? Quantity { get; set; }

        public bool HasOpts
        {
            get
            {
                return !string.IsNullOrEmpty(Container) || (!string.IsNullOrEmpty(Bin) && !string.IsNullOrEmpty(Item));
            }
        }

        /**************************************************
         * Singling: Src Ctr, Dest Ctr
         * Doubling: src ctr, dest bin
         * Cotainer Move ctr, bin
         * Bin to Bin:  Src bin, dest bin, src item, lot, qty
         *item to ctr: src bin, dest bin, dest ctr, src item, lot qty
         *item between ctr:  src bin, src ctr, dest bin, dest ctr, src item, src lot, src qty
         *full ctr to bin: src bin, src ctr, dest bin
         *remove from ctr: src bin, src ctr, dest bin dest ctr, item/lot/qty
         */

        public List<MenuLink> GetOpts()
        {
            var opts = new List<MenuLink>();
            if (!string.IsNullOrEmpty(Bin))
            {
                //opts.Add(new MenuLink(this, $"Move Bin: To {Bin}", "MoveBetweenBins"));
                //opts.Add(new MenuLink(this, $"Doubling: To {Bin}", "Doubling"));
            }

            if (!string.IsNullOrEmpty(Container) && string.IsNullOrEmpty(Item))
            {
                opts.Add(new MenuLink(this, $"Singling: To {Container}", "Singling", 
                    new { destination = Container }));
                opts.Add(new MenuLink(this, $"Singling: From {Container}", "Singling", 
                    new { source = Container }));
                opts.Add(new MenuLink(this, $"Doubling: From {Container}", "Doubling", 
                    new { source = Container }));
                opts.Add(new MenuLink(this, $"Move To Bin: From {Container}", "Container", 
                    new { ctr = Container }));
                opts.Add(new MenuLink(this, $"Move Full Ctr: From {Container}", "MoveContainerToBin",
                    new { ctr = Container }));
            }

            if (!string.IsNullOrEmpty(Container) && !string.IsNullOrEmpty(Bin))
            {
                //opts.Add(new MenuLink(this, $"Move Full Ctr: From {Bin}/{Container}", ""));
                //opts.Add(new MenuLink(this, $"Remove From Ctr: From {Bin}/{Container}", ""));
            }

            if (!string.IsNullOrEmpty(Item) && !string.IsNullOrEmpty(Bin))
            {
                opts.Add(new MenuLink(this, $"Move Bin: From {Bin} {Item}", "MoveBetweenBins", 
                    new { bin = Bin, item = Item, lot = Lot, qty = Quantity }));
                opts.Add(new MenuLink(this, $"Move {Item} To Ctr", "MoveItemsToContainer", 
                    new { bin = Bin, item = Item, lot = Lot, qty = Quantity }));
            }

            if (!string.IsNullOrEmpty(Item) && !string.IsNullOrEmpty(Container))
            {
                opts.Add(new MenuLink(this, $"Move {Item} Between Ctrs", "MoveItemsBetweenContainers",
                    new { item = Item, ctr = Container, lot = Lot, qty = Quantity }));
                opts.Add(new MenuLink(this, $"Remove {Item}", "RemoveContentOfContainer",
                    new { item = Item, ctr = Container, lot = Lot, qty = Quantity }));
            }

            return opts;
        }         
    }

    public class MenuLink
    {
        public MenuLink(MoveMenuModel parent, string label, string action, object args = null, string controller = "MaterialManagement")
        {
            Args = args ?? new
            {
                parent.Bin,
                parent.Item,
                parent.Lot,
                parent.Container,
                parent.Quantity
            };
            Label = label;
            Controller = controller;
            Action = action;
        }
        public string Label { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public object Args { get; set; }
    }
}


