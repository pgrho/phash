/*

pHash, the open source perceptual hash library
Copyright (C) 2009 Aetilius, Inc.
All rights reserved.

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

Evan Klinger - eklinger@phash.org
D Grant Starkweather - dstarkweather@phash.org

*/

using Shipwreck.Phash.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash
{
    //typedef struct ph_hash_point
    //{
    //    ulong hash;
    //    off_t index; /*pos of hash in orig file */
    //}
    //TxtHashPoint;

    //typedef struct ph_match
    //{
    //    off_t first_index; /* offset into first file */
    //    off_t second_index; /* offset into second file */
    //    uint32_t length;    /*length of match between 2 files */
    //}
    //TxtMatch;
    //typedef struct ph_file_offset
    //{
    //    off_t offset;
    //    byte fileno;
    //}
    //FileIndex;

    ///* structure for a single hash */
    //typedef struct ph_datapoint
    //{
    //    char* id;
    //    void* hash;
    //    float* path;
    //    uint32_t hash_length;
    //    byte hash_type;
    //}
    //DP;

    //typedef struct ph_slice
    //{
    //    DP** hash_p;
    //    int n;
    //    void* hash_params;
    //}
    //slice;

    //struct BinHash
    //{
    //    byte* hash;
    //    uint32_t bytelength;
    //    uint32_t byteidx; // used by addbit()
    //    byte bitmask;  // used by addbit()

    //    /*
    //	 * add a single bit to hash. the bits are
    //	 * written from left to right.
    //	 */
    //    int addbit(byte bit)
    //    {
    //        if (bitmask == 0)
    //        {
    //            bitmask = 128; // reset bitmask to "10000000"
    //            byteidx++;     // jump to next byte in array
    //        }

    //        if (byteidx >= bytelength) return -1;

    //        if (bit == 1) *(hash + byteidx) |= bitmask;
    //        bitmask >>= 1;
    //        return 0;
    //    }
    //};

    [Obsolete]
    public class pHash
    {
        //BinHash* _ph_bmb_new(uint32_t bytelength);
        //void ph_bmb_free(BinHash* binHash);

        ///* variables for textual hash */
        private const int KgramLength = 50;

        private const int WindowLength = 100;
        private const int delta = 1;

        //#define ROTATELEFT(x, bits)  (((x)<<(bits)) | ((x)>>(64-bits)))

        ///* /brief alloc a single data point
        // *  allocates path array, does nto set id or path
        // */
        //DP* ph_malloc_datapoint(int hashtype);

        ///** /brief free a datapoint and its path
        // *
        // */
        //void ph_free_datapoint(DP* dp);



        // /*! /brief compute dct robust image hash
        // *  /param file string variable for name of file
        // *  /param hash of type ulong (must be 64-bit variable)
        // *  /return int value - -1 for failure, 1 for success
        // */
        //int ph_dct_imagehash(const char* file, ulong &hash);

        //int ph_bmb_imagehash(const char* file, byte method, BinHash** ret_hash);
        //#endif



        ///* ! /brief dct video robust hash
        // *   Compute video hash based on the dct of normalized video 32x32x64 cube
        // *   /param file name of file
        // *   /param hash ulong value for hash value
        // *   /return int value - less than 0 for error
        // */
        //#ifdef HAVE_IMAGE_HASH
        //int ph_hamming_distance(const ulong hash1,const ulong hash2);

        //#endif


        ///** /brief textual hash for file
        // *  /param filename - char* name of file
        // *  /param nbpoints - int length of array of return value (out)
        // *  /return TxtHashPoint* array of hash points with respective index into file.
        // **/
        //TxtHashPoint* ph_texthash(const char* filename, int* nbpoints);

        ///** /brief compare 2 text hashes
        // *  /param hash1 -TxtHashPoint
        // *  /param N1 - int length of hash1
        // *  /param hash2 - TxtHashPoint
        // *  /param N2 - int length of hash2
        // *  /param nbmatches - int number of matches found (out)
        // *  /return TxtMatch* - list of all matches
        // **/
        //TxtMatch* ph_compare_text_hashes(TxtHashPoint* hash1, int N1, TxtHashPoint* hash2, int N2, int* nbmatches);

        /// <summary>
        /// random char mapping for textual hash
        /// </summary>
        private static readonly ulong[] textkeys = {
            15498727785010036736UL,
            7275080914684608512UL,
            14445630958268841984UL,
            14728618948878663680UL,
            16816925489502355456UL,
            3644179549068984320UL,
            6183768379476672512UL,
            14171334718745739264UL,
            5124038997949022208UL,
            10218941994323935232UL,
            8806421233143906304UL,
            11600620999078313984UL,
            6729085808520724480UL,
            9470575193177980928UL,
            17565538031497117696UL,
            16900815933189128192UL,
            11726811544871239680UL,
            13231792875940872192UL,
            2612106097615437824UL,
            11196599515807219712UL,
            300692472869158912UL,
            4480470094610169856UL,
            2531475774624497664UL,
            14834442768343891968UL,
            2890219059826130944UL,
            7396118625003765760UL,
            2394211153875042304UL,
            2007168123001634816UL,
            18426904923984625664UL,
            4026129272715345920UL,
            9461932602286931968UL,
            15478888635285110784UL,
            11301210195989889024UL,
            5460819486846222336UL,
            11760763510454222848UL,
            9671391611782692864UL,
            9104999035915206656UL,
            17944531898520829952UL,
            5395982256818880512UL,
            14229038033864228864UL,
            9716729819135213568UL,
            14202403489962786816UL,
            7382914959232991232UL,
            16445815627655938048UL,
            5226234609431216128UL,
            6501708925610491904UL,
            14899887495725449216UL,
            16953046154302455808UL,
            1286757727841812480UL,
            17511993593340887040UL,
            9702901604990058496UL,
            1587450200710971392UL,
            3545719622831439872UL,
            12234377379614556160UL,
            16421892977644797952UL,
            6435938682657570816UL,
            1183751930908770304UL,
            369360057810288640UL,
            8443106805659205632UL,
            1163912781183844352UL,
            4395489330525634560UL,
            17905039407946137600UL,
            16642801425058889728UL,
            15696699526515523584UL,
            4919114829672742912UL,
            9956820861803560960UL,
            6921347064588664832UL,
            14024113865587949568UL,
            9454608686614839296UL,
            12317329321407545344UL,
            9806407834332561408UL,
            724594440630435840UL,
            8072988737660780544UL,
            17189322793565552640UL,
            17170410068286373888UL,
            13299223355681931264UL,
            5244287645466492928UL,
            13623553490302271488UL,
            11805525436274835456UL,
            6531045381898240000UL,
            12688803018523541504UL,
            3061682967555342336UL,
            8118495582609211392UL,
            16234522641354981376UL,
            15296060347169898496UL,
            6093644486544457728UL,
            4223717250303000576UL,
            16479812286668603392UL,
            6463004544354746368UL,
            12666824055962206208UL,
            17643725067852447744UL,
            10858493883470315520UL,
            12125119390198792192UL,
            15839782419201785856UL,
            8108449336276287488UL,
            17044234219871535104UL,
            7349859215885729792UL,
            15029796409454886912UL,
            12621604020339867648UL,
            16804467902500569088UL,
            8900381657152880640UL,
            3981267780962877440UL,
            17529062343131004928UL,
            16973370403403595776UL,
            2723846500818878464UL,
            16252728346297761792UL,
            11825849685375975424UL,
            7968134154875305984UL,
            11429537762890481664UL,
            5184631047941259264UL,
            14499179536773545984UL,
            5671596707704471552UL,
            8246314024086536192UL,
            4170931045673205760UL,
            3459375275349901312UL,
            5095630297546883072UL,
            10264575540807598080UL,
            7683092525652901888UL,
            3128698510505934848UL,
            16727580085162344448UL,
            1903172507905556480UL,
            2325679513238765568UL,
            9139329894923108352UL,
            14028291906694283264UL,
            18165461932440551424UL,
            17247779239789330432UL,
            12625782052856266752UL,
            7068577074616729600UL,
            13830831575534665728UL,
            6800641999486582784UL,
            5426300911997681664UL,
            4284469158977994752UL,
            10781909780449460224UL,
            4508619181419134976UL,
            2811095488672038912UL,
            13505756289858273280UL,
            2314603454007345152UL,
            14636945174048014336UL,
            3027146371024027648UL,
            13744141225487761408UL,
            1374832156869656576UL,
            17526325907797573632UL,
            968993859482681344UL,
            9621146180956192768UL,
            3250512879761227776UL,
            4428369143422517248UL,
            14716776478503075840UL,
            13515088420568825856UL,
            12111461669075419136UL,
            17845474997598945280UL,
            11795924440611553280UL,
            14014634185570910208UL,
            1724410437128159232UL,
            2488510261825110016UL,
            9596182018555641856UL,
            1443128295859159040UL,
            1289545427904888832UL,
            3775219997702356992UL,
            8511705379065823232UL,
            15120377003439554560UL,
            10575862005778874368UL,
            13938006291063504896UL,
            958102097297932288UL,
            2911027712518782976UL,
            18446625472482639872UL,
            3769197585969971200UL,
            16416784002377056256UL,
            2314484861370368000UL,
            18406142768607920128UL,
            997186299691532288UL,
            16058626086858129408UL,
            1334230851768025088UL,
            76768133779554304UL,
            17027619946340810752UL,
            10955377032724217856UL,
            3327281022130716672UL,
            3009245016053776384UL,
            7225409437517742080UL,
            16842369442699542528UL,
            15120706693719130112UL,
            6624140361407135744UL,
            10191549809601544192UL,
            10688596805580488704UL,
            8348550798535294976UL,
            12680060080016588800UL,
            1838034750426578944UL,
            9791679102984388608UL,
            13969605507921477632UL,
            5613254748128935936UL,
            18303384482050211840UL,
            10643238446241415168UL,
            16189116753907810304UL,
            13794646699404165120UL,
            11601340543539347456UL,
            653400401306976256UL,
            13794528098177253376UL,
            15370538129509318656UL,
            17070184403684032512UL,
            16109012959547621376UL,
            15329936824407687168UL,
            18067370711965499392UL,
            13720894972696199168UL,
            16664167676175712256UL,
            18144138845745053696UL,
            12301770853917392896UL,
            9172800635190378496UL,
            3024675794166218752UL,
            15311015869971169280UL,
            16398210081298055168UL,
            1420301171746144256UL,
            11984978489980747776UL,
            4575606368995639296UL,
            11611850981347688448UL,
            4226831221851684864UL,
            12924157176120868864UL,
            5845166987654725632UL,
            6064865972278263808UL,
            4269092205395705856UL,
            1368028430456586240UL,
            11678120728997134336UL,
            4125732613736366080UL,
            12011266876698001408UL,
            9420493409195393024UL,
            17920379313140531200UL,
            5165863346527797248UL,
            10073893810502369280UL,
            13268163337608232960UL,
            2089657402327564288UL,
            8697334149066784768UL,
            10930432232036237312UL,
            17419594235325186048UL,
            8317960787322732544UL,
            6204583131022884864UL,
            15637017837791346688UL,
            8015355559358234624UL,
            59609911230726144UL,
            6363074407862108160UL,
            11040031362114387968UL,
            15370625789791830016UL,
            4314540415450611712UL,
            12460332533860532224UL,
            8908860206063026176UL,
            8890146784446251008UL,
            5625439441498669056UL,
            13135691436504645632UL,
            3367559886857568256UL,
            11470606437743329280UL,
            753813335073357824UL,
            7636652092253274112UL,
            12838634868199915520UL,
            12431934064070492160UL,
            11762384705989640192UL,
            6403157671188365312UL,
            3405683408146268160UL,
            11236019945420619776UL,
            11569021017716162560UL
        };

        [Obsolete]
        internal static Projections ph_radon_projections(FloatImage img, int N)
            => ImagePhash.FindRadonProjections(img, N);

        [Obsolete]
        internal static Features ph_feature_vector(Projections projs)
            => ImagePhash.ComputeFeatureVector(projs);

        [Obsolete]
        public static Digest ph_dct(Features fv)
            => ImagePhash.ComputeDct(fv);

        [Obsolete]
        public static bool ph_crosscorr(Digest x, Digest y, out double pcc, double threshold = 0.9)
        {
            pcc = ImagePhash.GetCrossCorrelation(x, y);
            return pcc > threshold;
        }

        [Obsolete]
        internal static Digest _ph_image_digest(ByteImage img, double sigma, double gamma, int numberOfAngles = 180)
            => ImagePhash.ComputeDigest(img, sigma, gamma, numberOfAngles: numberOfAngles);

        [Obsolete]
        public static bool _ph_compare_images(BitmapSource imA, BitmapSource imB, out double pcc, double sigma = 3.5, double gamma = 1.0, int N = 180, double threshold = 0.90)
            => ImagePhash.CompareImages(imA, imB, out pcc, sigma: sigma, gamma: gamma, numberOfAngles: N, threshold: threshold);

        [Obsolete]
        public static bool ph_compare_images(string file1, string file2, out double pcc, double sigma = 3.5, double gamma = 1.0, int N = 180, double threshold = 0.90)
            => ImagePhash.CompareImages(file1, file2, out pcc, sigma: sigma, gamma: gamma, numberOfAngles: N, threshold: threshold);

        [Obsolete]
        private static FloatImage ph_dct_matrix(int N)
            => ImagePhash.CreateDctMatrix(N);

        //BinHash* _ph_bmb_new(uint32_t bytelength)
        //{
        //    BinHash* bh = (BinHash*)malloc(sizeof(BinHash));
        //    bh->bytelength = bytelength;
        //    bh->hash = (byte*)calloc(sizeof(byte), bytelength);
        //    bh->byteidx = 0;
        //    bh->bitmask = 128;
        //    return bh;
        //}

        //void ph_bmb_free(BinHash* bh)
        //{
        //    if (bh)
        //    {
        //        free(bh->hash);
        //        free(bh);
        //    }
        //}
        //int ph_bmb_imagehash(const char* file, byte method, BinHash** ret_hash)
        //{
        //    CImg<byte> img;
        //    const byte* ptrsrc;  // source pointer (img)
        //    byte* block;
        //    int pcol;  // "pointer" to pixel col (x)
        //    int prow;  // "pointer" to pixel row (y)
        //    int blockidx = 0;  //current idx of block begin processed.
        //    double median;  // median value of mean_vals
        //    const int preset_size_x = 256;
        //    const int preset_size_y = 256;
        //    const int blk_size_x = 16;
        //    const int blk_size_y = 16;
        //    int pixcolstep = blk_size_x;
        //    int pixrowstep = blk_size_y;

        //    int number_of_blocks;
        //    uint32_t bitsize;
        //    // number of bytes needed to store bitsize bits.
        //    uint32_t bytesize;

        //    if (!file || !ret_hash)
        //    {
        //        return -1;
        //    }
        //    try
        //    {
        //        img.load(file);
        //    }
        //    catch (CImgIOException ex)
        //    {
        //        return -1;
        //    }

        //    const int blk_size = blk_size_x * blk_size_y;
        //    block = (byte*)malloc(sizeof(byte) * blk_size);

        //    if (!block)
        //        return -1;

        //    switch (img.spectrum())
        //    {
        //        case 3: // from RGB
        //            img.RGBtoYCbCr().channel(0);
        //            break;
        //        default:
        //            *ret_hash = null;
        //            free(block);
        //            return -1;
        //    }

        //    img.resize(preset_size_x, preset_size_y);

        //    // ~step b
        //    ptrsrc = img.data();  // set pointer to beginning of pixel buffer

        //    if (method == 2)
        //    {
        //        pixcolstep /= 2;
        //        pixrowstep /= 2;

        //        number_of_blocks =
        //            ((preset_size_x / blk_size_x) * 2 - 1) *
        //            ((preset_size_y / blk_size_y) * 2 - 1);
        //    }
        //    else
        //    {
        //        number_of_blocks =
        //            preset_size_x / blk_size_x *
        //            preset_size_y / blk_size_y;
        //    }

        //    bitsize = number_of_blocks;
        //    bytesize = bitsize / 8;

        //    double* mean_vals = new double[number_of_blocks];

        //    /*
        //    * pixel row < block < block row < image
        //    *
        //    * The pixel rows of a block are copied consecutively
        //    * into the block buffer (using memcpy). When a block is
        //    * finished, the next block in the block row is processed.
        //    * After finishing a block row, the processing of the next
        //    * block row is started. An image consists of an arbitrary
        //    * number of block rows.
        //    */

        //    /* image (multiple rows of blocks) */
        //    for (prow = 0; prow <= preset_size_y - blk_size_y; prow += pixrowstep)
        //    {
        //        /* block row */
        //        for (pcol = 0; pcol <= preset_size_x - blk_size_x; pcol += pixcolstep)
        //        {
        //            // idx for array holding one block.
        //            int blockpos = 0;

        //            /* block */

        //            // i is used to address the different
        //            // pixel rows of a block
        //            for (int i = 0; i < blk_size_y; i++)
        //            {
        //                ptrsrc = img.data(pcol, prow + i);
        //                memcpy(block + blockpos, ptrsrc, blk_size_x);
        //                blockpos += blk_size_x;
        //            }

        //            mean_vals[blockidx] = CImg<byte>(block, blk_size).mean();
        //            blockidx++;

        //        }
        //    }

        //    /* calculate the median */
        //    median = CImg<double>(mean_vals, number_of_blocks).median();

        //    /* step e */
        //    BinHash* hash = _ph_bmb_new(bytesize);

        //    if (!hash)
        //    {
        //        *ret_hash = null;
        //        return -1;
        //    }

        //    *ret_hash = hash;
        //    for (uint32_t i = 0; i < bitsize; i++)
        //    {
        //        if (mean_vals[i] < median)
        //        {
        //            hash->addbit(0);
        //        }
        //        else
        //        {
        //            hash->addbit(1);
        //        }
        //    }
        //    delete[] mean_vals;
        //    free(block);
        //    return 0;
        //}

        [Obsolete]
        public static ulong ph_dct_imagehash(Stream file)
            => ImagePhash.ComputeDctHash(file);

        public static int ph_hamming_distance(ulong hash1, ulong hash2)
        {
            ulong x = hash1 ^ hash2;
            const ulong m1 = 0x5555555555555555UL;
            const ulong m2 = 0x3333333333333333UL;
            const ulong h01 = 0x0101010101010101UL;
            const ulong m4 = 0x0f0f0f0f0f0f0f0fUL;
            x -= (x >> 1) & m1;
            x = (x & m2) + ((x >> 2) & m2);
            x = (x + (x >> 4)) & m4;
            return (int)((x * h01) >> 56);
        }

        [Obsolete]
        private static FloatImage GetMHKernel(float alpha, float level)
            => ImagePhash.CreateMHKernel(alpha, level);

        ///** /brief create MH image hash for filename image
        //*   /param filename - string name of image file
        //*   /param N - (out) int value for length of image hash returned
        //*   /param alpha - int scale factor for marr wavelet (default=2)
        //*   /param lvl   - int level of scale factor (default = 1)
        //*   /return byte array
        //**/
        byte[] ph_mh_imagehash(Stream filename, out int N, float alpha = 2.0f, float lvl = 1.0f)
        {
            var hash = new byte[N = 72];

            var blurred = BitmapFrame.Create(filename, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).ToByteImageOfYOrB().Convolve(FloatImage.CreateGaussian(2, 1.0));
            var resized = blurred.Resize(512, 512); // TODO: bicubic
            var equalized = resized;// TODO: histogram equalization

            throw new NotImplementedException();
            //    CImg<float>* pkernel = GetMHKernel(alpha, lvl);
            //    CImg<float> fresp = img.get_correlate(*pkernel);
            //    img.clear();
            //    fresp.normalize(0, 1.0);
            //    CImg<float> blocks(31, 31, 1, 1, 0);
            //    for (int rindex = 0; rindex < 31; rindex++)
            //    {
            //        for (int cindex = 0; cindex < 31; cindex++)
            //        {
            //            blocks(rindex, cindex) = (float)fresp.get_crop(rindex * 16, cindex * 16, rindex * 16 + 16 - 1, cindex * 16 + 16 - 1).sum();
            //        }
            //    }
            //    int hash_index;
            //    int nb_ones = 0, nb_zeros = 0;
            //    int bit_index = 0;
            //    unsigned char hashbyte = 0;
            //    for (int rindex = 0; rindex < 31 - 2; rindex += 4)
            //    {
            //        CImg<float> subsec;
            //        for (int cindex = 0; cindex < 31 - 2; cindex += 4)
            //        {
            //            subsec = blocks.get_crop(cindex, rindex, cindex + 2, rindex + 2).unroll('x');
            //            float ave = (float)subsec.mean();
            //            cimg_forX(subsec, I){
            //                hashbyte <<= 1;
            //                if (subsec(I) > ave)
            //                {
            //                    hashbyte |= 0x01;
            //                    nb_ones++;
            //                }
            //                else
            //                {
            //                    nb_zeros++;
            //                }
            //                bit_index++;
            //                if ((bit_index % 8) == 0)
            //                {
            //                    hash_index = (int)(bit_index / 8) - 1;
            //                    hash[hash_index] = hashbyte;
            //                    hashbyte = 0x00;
            //                }
            //            }
            //        }
            //    }

            //    return hash;
        }
        //#endif

        /// <summary>
        /// brief count number bits set in given byte.
        /// </summary>
        /// <param name="val">byte value.</param>
        /// <returns>int value for number of bits set.</returns>
        public static int ph_bitcount8(byte val)
        {
            int num = 0;
            while (val != 0)
            {
                ++num;
                val = (byte)(val & (val - 1));
            }
            return num;
        }

        /// <summary>
        /// brief compute hamming distance between two byte arrays
        /// </summary>
        /// <param name="hashA">byte array for first hash</param>
        /// <param name="lenA">int length of hashA</param>
        /// <param name="hashB">byte array for second hash</param>
        /// <param name="lenB">int length of hashB</param>
        /// <returns>double value for normalized hamming distance.</returns>
        public unsafe static double ph_hammingdistance2(byte* hashA, int lenA, byte* hashB, int lenB)
        {
            if (lenA != lenB)
            {
                return -1.0;
            }
            if ((hashA == null) || (hashB == null) || (lenA <= 0))
            {
                return -1.0;
            }
            double dist = 0;
            byte D = 0;
            for (int i = 0; i < lenA; i++)
            {
                D = (byte)(hashA[i] ^ hashB[i]);
                dist = dist + ph_bitcount8(D);
            }
            double bits = (double)lenA * 8;
            return dist / bits;
        }

        //TxtHashPoint* ph_texthash(const char* filename, int* nbpoints)
        //{
        //    int count;
        //    TxtHashPoint* TxtHash = null;
        //    TxtHashPoint WinHash[WindowLength];
        //    char kgram[KgramLength];

        //    FILE* pfile = fopen(filename, "r");
        //    if (!pfile)
        //    {
        //        return null;
        //    }
        //    struct stat fileinfo;
        //    fstat(fileno(pfile),&fileinfo);
        //    count = fileinfo.st_size - WindowLength + 1;
        //    count = (int)(0.01* count);
        //    int d;
        //ulong hashword = 0ULL;

        //    TxtHash = (TxtHashPoint*)malloc(count*sizeof(struct ph_hash_point));
        //    if (!TxtHash){
        //        return null;
        //    }
        //    * nbpoints = 0;
        //int i, first = 0, last = KgramLength - 1;
        //int text_index = 0;
        //int win_index = 0;
        //    for (i=0;i<KgramLength;i++){    /* calc first kgram */
        //        d = fgetc(pfile);
        //        if (d == EOF){
        //            free(TxtHash);
        //            return null;
        //        }
        //        if (d <= 47)         /*skip cntrl chars*/
        //            continue;
        //        if ( ((d >= 58)&&(d <= 64)) || ((d >= 91)&&(d <= 96)) || (d >= 123) ) /*skip punct*/
        //            continue;
        //        if ((d >= 65)&&(d<=90))       /*convert upper to lower case */
        //            d = d + 32;

        //        kgram[i] = (char)d;
        //        hashword = hashword << delta;   /* rotate left or shift left ??? */
        //        hashword = hashword^textkeys[d];/* right now, rotate breaks it */
        //    }

        //    WinHash[win_index].hash = hashword;
        //    WinHash[win_index++].index = text_index;
        //    struct ph_hash_point minhash;
        //    minhash.hash = ULLONG_MAX;
        //    minhash.index = 0;
        //    struct ph_hash_point prev_minhash;
        //    prev_minhash.hash = ULLONG_MAX;
        //    prev_minhash.index = 0;

        //    while ((d=fgetc(pfile)) != EOF){    /*remaining kgrams */
        //        text_index++;
        //        if (d == EOF){
        //            free(TxtHash);
        //            return null;
        //        }
        //        if (d <= 47)         /*skip cntrl chars*/
        //            continue;
        //        if ( ((d >= 58)&&(d <= 64)) || ((d >= 91)&&(d <= 96)) || (d >= 123) ) /*skip punct*/
        //            continue;
        //        if ((d >= 65)&&(d<=90))       /*convert upper to lower case */
        //            d = d + 32;

        //        ulong oldsym = textkeys[kgram[first % KgramLength]];

        ///* rotate or left shift ??? */
        ///* right now, rotate breaks it */
        //oldsym = oldsym << delta* KgramLength;
        //hashword = hashword << delta;
        //        hashword = hashword^textkeys[d];
        //        hashword = hashword^oldsym;
        //        kgram[last % KgramLength] = (char)d;
        //        first++;
        //        last++;

        //        WinHash[win_index % WindowLength].hash = hashword;
        //        WinHash[win_index % WindowLength].index = text_index;
        //        win_index++;

        //        if (win_index >= WindowLength){
        //            minhash.hash = ULLONG_MAX;
        //            for (i=win_index;i<win_index+WindowLength;i++){
        //                if (WinHash[i % WindowLength].hash <= minhash.hash){
        //                    minhash.hash = WinHash[i % WindowLength].hash;
        //                    minhash.index = WinHash[i % WindowLength].index;
        //                }
        //            }
        //            if (minhash.hash != prev_minhash.hash){
        //                TxtHash[(*nbpoints)].hash = minhash.hash;
        //                TxtHash[(*nbpoints)++].index = minhash.index;
        //                prev_minhash.hash = minhash.hash;
        //                prev_minhash.index = minhash.index;

        //            } else {
        //                TxtHash[*nbpoints].hash = prev_minhash.hash;
        //                TxtHash[(*nbpoints)++].index = prev_minhash.index;
        //            }
        //            win_index = 0;
        //        }
        //    }

        //    fclose(pfile);
        //    return TxtHash;
        //}

        //TxtMatch* ph_compare_text_hashes(TxtHashPoint* hash1, int N1, TxtHashPoint* hash2, int N2, int* nbmatches)
        //{
        //    int max_matches = (N1 >= N2) ? N1 : N2;
        //    TxtMatch* found_matches = (TxtMatch*)malloc(max_matches * sizeof(TxtMatch));
        //    if (!found_matches)
        //    {
        //        return null;
        //    }

        //    *nbmatches = 0;
        //    int i, j;
        //    for (i = 0; i < N1; i++)
        //    {
        //        for (j = 0; j < N2; j++)
        //        {
        //            if (hash1[i].hash == hash2[j].hash)
        //            {
        //                int m = i + 1;
        //                int n = j + 1;
        //                int cnt = 1;
        //                while ((m < N1) && (n < N2) && (hash1[m++].hash == hash2[n++].hash))
        //                {
        //                    cnt++;
        //                }
        //                found_matches[*nbmatches].first_index = i;
        //                found_matches[*nbmatches].second_index = j;
        //                found_matches[*nbmatches].length = cnt;
        //                (*nbmatches)++;
        //            }
        //        }
        //    }
        //    return found_matches;
        //}
    }
}