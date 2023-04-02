class TokoApp
{
    static void Main(string[] args)
    {
        Toko toko = new Toko();

        EventProduct e = new EventProduct();
        e.ProcesCompleted += OnProccesCompleted;

        void OnProccesCompleted(object sender, string message)
        {
            Console.WriteLine(message);
        }

        while (true)
        {
            toko.TampilanMenu();
            Console.Write("Masukan nomor pilihan menu :");
            int pilihan = int.Parse(Console.ReadLine());

            switch (pilihan)
            {
                case 1:
                    toko.TambahProduk();
                    e.Trigger("Produk ditambahkan...");
                    break;
                case 2:
                    toko.TampilkanProduk();
                    toko.EditProduk();
                    break;
                case 3:
                    toko.TampilkanProduk();
                    toko.HapusProduk();
                    break;
                case 4:
                    toko.TampilkanProduk();
                    e.Trigger("Menampilkan Produk...");
                    break;
                case 5:
                    toko.TampilkanKeranjang();
                    toko.TambahKeranjang();
                    break;
                case 6:
                    toko.TampilkanKeranjang();
                    toko.HapusKeranjang();
                    break;
                case 7:
                    toko.TampilkanKeranjang();
                    e.Trigger("Menampilkan Keranjang...");
                    break;
                case 8:
                    toko.Checkout();
                    break;
                case 9:
                    toko.Keluar();
                    break;
                default:
                    Console.WriteLine("Pilihan menu salah !");
                    break;
            }
        }
    }
}

class Produk
{
    public string Sku { get; set; }
    public string Nama { get; set; }
    public int Stok { get; set; }
    public int Harga { get; set; }
}

class Keranjang
{
    public string Sku { get; set; }
    public string Nama { get; set; }
    public int Quantity { get; set; }
    public int Harga { get; set; }
}

class Toko
{
    List<Produk> produkList = new List<Produk>();
    List<Keranjang> keranjangList = new List<Keranjang>();
    public void TambahProduk()
    {
        Console.Clear();
        Console.WriteLine("===== Tambah Produk =====");
        Console.Write("SKU : ");
        string sku = Console.ReadLine();
        Console.Write("Nama : ");
        string nama = Console.ReadLine();
        Console.Write("Stok : ");
        int stok = int.Parse(Console.ReadLine());
        Console.Write("Harga : ");
        int harga = int.Parse(Console.ReadLine());

        Produk produkBaru = new Produk();
        produkBaru.Sku = sku;
        produkBaru.Nama = nama;
        produkBaru.Stok = stok;
        produkBaru.Harga = harga;
        produkList.Add(produkBaru);
    }

    public void EditProduk()
    {
        Console.Clear();
        TampilkanProduk();
        Console.WriteLine("===== EDIT Produk =====");
        Console.Write("SKU : ");
        string sku = Console.ReadLine();
        Console.Write("Nama : ");
        string namaBaru = Console.ReadLine();
        Console.Write("Stok : ");
        int stokBaru = int.Parse(Console.ReadLine());
        Console.Write("Harga : ");
        int hargaBaru = int.Parse(Console.ReadLine());

        Produk produk = null;
        foreach (Produk p in produkList)
        {
            if (p.Sku == sku)
            {
                produk = p;
                break;
            }
        }
        if (produk != null)
        {
            produk.Nama = namaBaru;
            produk.Stok = stokBaru;
            produk.Harga = hargaBaru;
        }
        else
        {
            Console.WriteLine($"Produk dengan {sku} tidak ditemukan !");
        }
    }

    public void HapusProduk()
    {
        Console.Clear();
        TampilkanProduk();
        Console.WriteLine("===== Hapus Produk =====");
        Console.Write("SKU: ");
        string sku = Console.ReadLine();
        Produk produk = null;

        foreach (Produk p in produkList)
        {
            if (p.Sku == sku)
            {
                produk = p;
                break;
            }
        }
        if (produk != null)
        {
            produkList.Remove(produk);
        }
        else
        {
            Console.WriteLine($"Produk dengan {sku} tidak ditemukan !");
        }
    }

    public void TampilkanProduk()
    {
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine("                 MENAMPILKAN DAFTAR PRODUK       ");
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine("SKU\t|\tNAMA\t|\tSTOK\t|\tHARGA\t|");

        foreach (Produk p in produkList)
        {
            Console.WriteLine($"{p.Sku}\t|\t{p.Nama}\t|\t{p.Stok}\t|\t{p.Harga}\t|");
        }
    }

    public void TampilkanKeranjang()
    {
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine("                 MENAMPILKAN DAFTAR KERANJANG       ");
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine("SKU\t|\tNAMA\t|\tSTOK\t|\tHARGA\t|");

        foreach (Keranjang k in keranjangList)
        {
            Console.WriteLine($"{k.Sku}\t|\t{k.Nama}\t|\t{k.Quantity}\t|\t{k.Harga}\t|");
        }
    }

    public void TampilanMenu()
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine("              MENU           ");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("  1.TAMBAH PRODUK");
        Console.WriteLine("  2.EDIT PRODUK");
        Console.WriteLine("  3.HAPUS PRODUK");
        Console.WriteLine("  4.TAMPILKAN PRODUK");
        Console.WriteLine("  5.TAMBAH PRODUK KE KERANJANG");
        Console.WriteLine("  6.HAPUS PRODUK DI KERANJANG");
        Console.WriteLine("  7.TAMPILKAN KERANJANG");
        Console.WriteLine("  8.CHECKOUT");
        Console.WriteLine("  9.KELUAR");
    }

    public void TambahKeranjang()
    {
        Console.Clear();
        Console.WriteLine("===== Tambah Keranjang =====");
        Console.Write("SKU : ");
        string sku = Console.ReadLine();
        Console.Write("Quantity : ");
        int quantity = int.Parse(Console.ReadLine());

        Produk produk = null;

        foreach (Produk p in produkList)
        {
            produk = p;
            break;
        }
        if (produk.Sku == sku && produk.Stok > quantity)
        {
            Keranjang newKeranjang = new Keranjang()
            {
                Sku = produk.Sku,
                Nama = produk.Nama,
                Quantity = quantity,
                Harga = produk.Harga
            };

            keranjangList.Add(newKeranjang);

            for (int i = 0; i < produkList.Count; i++)
            {
                if (produkList[i].Sku == sku)
                {
                    produkList[i].Stok -= quantity;
                    break;
                }
            }
        }
        else if (produk.Sku == sku && produk.Stok < quantity)
        {
            Console.WriteLine("Quantity melebihi stok !");
        }
        else
        {
            Console.WriteLine("Produk Tidak ditemukan !");
        }
    }

    public void HapusKeranjang()
    {
        Console.Clear();
        Console.WriteLine("===== Hapus Keranjang =====");
        Console.Write("SKU : ");
        string sku = Console.ReadLine();
        Console.Write("Quantity : ");
        int quantity = int.Parse(Console.ReadLine());

        Keranjang keranjang = null;

        try
        {
            foreach (Keranjang k in keranjangList)
            {
                if (k.Sku == sku && k.Quantity == quantity)
                {
                    keranjang = k;
                    keranjangList.Remove(keranjang);
                    Console.WriteLine("Produk telah dihapus dari keranjang");
                    for (int i = 0; i < produkList.Count; i++)
                    {
                        if (produkList[i].Sku == sku)
                        {
                            produkList[i].Stok += quantity;
                            break;
                        }
                    }
                }
                else if (k.Sku == sku && k.Quantity > quantity)
                {
                    for (int i = 0; i < keranjangList.Count; i++)
                    {
                        if (keranjangList[i].Sku == sku)
                        {
                            keranjangList[i].Quantity -= quantity;
                            break;
                        }
                    }
                    for (int i = 0; i < produkList.Count; i++)
                    {
                        if (produkList[i].Sku == sku)
                        {
                            produkList[i].Stok += quantity;
                            break;
                        }
                    }
                }
                else if (k.Sku == sku && k.Quantity < quantity)
                {
                    Console.WriteLine("Melebihi quantity produk dikeranjang");
                }
                else
                {
                    Console.WriteLine("Produk Tidak ditemukan!");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Checkout()
    {
        Console.Clear();
        Console.WriteLine("BAYAR atau BATAL ? ");
        Console.WriteLine("Jika BAYAR tekan 1 jika BATAL tekan 0 ");
        int pilih = int.Parse(Console.ReadLine());

        if (pilih == 1)
        {
            int total = 0;
            for (int i = 0; i < keranjangList.Count; i++)
            {
                int harga = keranjangList[i].Harga * keranjangList[i].Quantity;
                total += harga;

            }
            Console.WriteLine("Total Bayar : {0}", total);
        }
        else
        {
            Console.WriteLine("CHECKOUT DIBATALKAN ");
        }
    }

    public void Keluar()
    {
        Console.WriteLine("Berhasil keluar aplikasi...");
        Environment.Exit(0);
    }
}

class EventProduct
{
    public event EventHandler<String> ProcesCompleted;
    public void Trigger(string message)
    {
        OnProccesCompleted(message);
    }
    protected virtual void OnProccesCompleted(string message)
    {
        ProcesCompleted?.Invoke(this, message);
    }
}