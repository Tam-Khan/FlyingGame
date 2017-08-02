using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System;
using FlyingGame.Model.Enemy;
using FlyingGame.Model.Enemy.Boss;
using FlyingGame.Model.Enemy.Helis;
using FlyingGame.Model.Enemy.Jet;
using FlyingGame.Model.MyJet;
using FlyingGame.Model.Shared;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using Pen = System.Drawing.Pen;

namespace FlyingGame
{
    public partial class MainConsole : Form
    {
        #region declarations
        //Sound paths
        private const string Gunfire = @"G:\VSprojects\FlyingGame\FlyingGame\Sounds\fire.wav";
        private const string BombAway = @"G:\VSprojects\FlyingGame\FlyingGame\Sounds\BombAway.wav";
        private const string BombXplode = @"G:\VSprojects\FlyingGame\FlyingGame\Sounds\BombXplode.wav";
        private const string PowerUp = @"G:\VSprojects\FlyingGame\FlyingGame\Sounds\tada.wav";
        private const string EnemyXplode = @"G:\VSprojects\FlyingGame\FlyingGame\Sounds\EnemyXplode.wav";
        private const string NoseBombCharge = @"G:\VSprojects\FlyingGame\FlyingGame\Sounds\Beep.wav";

        //Instantiate required objects
        private readonly Random _rand = new Random();
        
        //For game control
        private GameController _gc = new GameController();
        //For keyboard input
        private KeyboardInput _ki = new KeyboardInput();

        //For ground mountain background
        private GroundObject _go = new GroundObject();
        
        //For cloud background
        private SkyObjects _so = new SkyObjects();
        private List<CloudFactory> _cL = new List<CloudFactory>();              //Create list of clouds for multiple clouds generation
        
        //For my jet
        private MyJet _mj = new MyJet();
        private MyJetBomb _mjb = new MyJetBomb();
        private List<MyJetGunBullet> _mjgb = new List<MyJetGunBullet>();         //Create list of bullets for multiple bullets generation
        private Pen _myJetBulletColor;
        
        //For power ups 
        private PowerUps _pu = new PowerUps();
        
        //For enemy jet
        private List<SmallJet> _ej = new List<SmallJet>();                      //Create list of enemy jets for multiple jets generation
        private List<EnemyBullet> _ejb = new List<EnemyBullet>();               //Create list of bullets for multiple bullets generation
        
        //For enemy helis
        private List<SmallHeli> _eH = new List<SmallHeli>();                    //Create list of enemy helis for multiple heli generation

        //For explosion animation
        private List<Explosion> _exp = new List<Explosion>();           

        //For boss
        private PlaneBoss _eB1 = new PlaneBoss();
        private RocketBoss _eB2 = new RocketBoss();

        private byte _dayPhase;
        #endregion

        public MainConsole()
        {
            InitializeComponent();
            _mj.MovementState = 0;
            _ki.KeyDirectionVertical = 0;
            _gc.IsGameOver = true;
            LblStartEndBanner.Visible = true;
            LblStartEndBanner.Text = @"Hit Enter to start the game.";
            
            timer1.Interval = 1000 / _gc.GameSpeed;
            timer1.Tick += UpdateSky;
            timer1.Start();
        }

        private void StartGame()
        {
            _gc = new GameController();
            _ki = new KeyboardInput();

            _go = new GroundObject();
            _so = new SkyObjects();

            _mj = new MyJet();
            _mjb = new MyJetBomb();
            _pu = new PowerUps();
            _eB1 = new PlaneBoss();
            _eB2 = new RocketBoss();

            if (_gc.IsFullPowered)
            {
                _mj.Hp = 10;
                _mjb.BombRemains = 5;
                _mj.CurrentActiveGun = 3;
                _mj.CurrentBulletLimit = 10;
            }

            _myJetBulletColor = new Pen(Brushes.Blue);
            BackColor = Color.Empty;

            //Clear all lists when game restarts
            _ej.Clear();
            _ejb.Clear();
            _mjgb.Clear();
            _exp.Clear();
            _cL.Clear();
            _eH.Clear();
        }
        
        private void UpdateSky(object sender, EventArgs e)
        {
            Graphics sky = PbConsole.CreateGraphics();

            StartRoutine();

            if (_gc.IsGameOver)
            {
                #region RestartGame
                if (_ki.RestartGame)
                {
                    LblInfo.Visible = false;
                    LblStartEndBanner.Visible = false;
                    LblBoss.Visible = false;
                    LblBossRem.Visible = false;
                    StartGame();
                    _ki.RestartGame = false;
                }
                #endregion
            }
            else
            {
                if (_mjb.BombXploded) goto BombExploded; //no other action/drawing till the bomb explosion finished.

                #region DayNight

                //whole canvas must be cleared before round starts

                if (_gc.DayPhase == 2)              //dusk
                {
                    sky.Clear(Color.LightPink);
                    _dayPhase = 2;
                }

                else if (_gc.DayPhase == 3)         //night
                {
                    sky.Clear(Color.DarkBlue);
                    _dayPhase = 3;
                }
                else if (_gc.DayPhase == 4)         //dawn
                {
                    sky.Clear(Color.LightCyan);
                    _dayPhase = 4;
                }
                else
                {
                    sky.Clear(Color.LightSkyBlue);  //normal day time
                    _dayPhase = 1;
                }

                #endregion
                
                #region Ground
                //Initiate
                if (_rand.Next(1, 10) == 5 && !_go.MountainDrawInitiated)
                    InitiateMountain();
                
                if (!_go.MountainDrawInitiated) goto MyJetRegion;           //no mountain to draw, bypass this region

                //Draw
                sky.FillPolygon(_go.MountainColor, _go.MountainPoints());
                
                //Post draw process
                _go.RefX = _go.RefX - _go.MountainMoveDeltaX;
                if (_go.RefX + _go.Width < 0) _go = new GroundObject();

                #endregion

                #region Cloud

                if(_gc.SkyobjectOff) goto MyJetRegion;

                //only initiate when existing cloud is lesser than 4 and no incompleted cloud (to avoid major overlap between clouds)
                if (_rand.Next(1, 150) == 50 && _cL.Count < 4 && (_cL.Select(x => x).Where(x => !x.IsGenerated).Count() == 0))
                    InitiateCloud();
                
                if(_cL.Count==0) goto MyJetRegion;  //No cloud to process, leave this region

                //draw and post draw process
                DrawCloudAndProcessData(sky);

                #endregion

                #region MyJet

            MyJetRegion:
                
                //jet gun fire - only when existing bullet is lesser than set limit (per active gun)
                if (_ki.KeyFire && _mjgb.Count < _mj.CurrentBulletLimit * _mj.CurrentActiveGun)
                    FireGun_MyJet();

                BombDrop_MyJet(sky);
                
                GetMyJetCurrentDirection();
                
                KeepMyJetWithinCanvas();

                DrawMyJet(sky);

                #region Myjet GunFire
                //animate jet gunfire
                if (_mjgb.Select(x => x).Where(x => x.GunFireStart).Count() > 0)
                {
                    for (int i = 0; i < _mjgb.Count; i++)
                    {
                        DrawMyJetBulletAndProcessData(sky, i);
                        
                        //Attack enemy
                        AttackEnemyJet(i);
                        AttackEnemyHeli(i);
                        
                        //Attack boss
                        if (_gc.BossType == 1 && _eB1.IsBossInitiated && _eB1.CurrHp > 0)
                        {
                            AttackEnemyBossType1(i);
                        }
                        else if(_gc.BossType == 2 && _eB2.IsBossInitiated && _eB2.CurrHp > 0)
                        {
                            AttackEnemyBossType2(i);   
                        }
                    }
                }

                #endregion

                if (_gc.InLevelTransition)      //Do not draw/perform any further, wait till level transition completes.
                {
                    LevelCompleted();
                    goto EndTasks;
                }
                #endregion

                #region EnemyJet

                InitiateEnemyJets();
                
                if (_ej.Count == 0) goto EnemyHeli;                //no enemy jet to process, skip this region

                //Perform all processes for each enemy jet like move, draw, process, fire bullets blah blah
                for (int i = 0; i < _ej.Count; i++)
                {
                    ProcessEnemyJetDirectionAndMovement(i);

                    DrawEnemyJet(sky,i);
                    
                    EnemyJetAndMyJetCollision(i);

                    FireEnemyGun(true, i);
                }
                
                #endregion
            
                #region EnemyHeli
            
            EnemyHeli:

                InitiateEnemyHeli();

                if (_eH.Count == 0) goto Powerups;  //no enemy to process, skip this region

                //Perform all processes for each enemy heli like move, draw, process, fire bullets blah blah
                for (int i = 0; i < _eH.Count; i++)
                {
                    ProcessEnemyHeliDirection(i);

                    ProcessEnemyHeliMovement(i);

                    DrawEnemyHeli(sky,i);

                    EnemyHeliAndMyJetCollision(i);
                    
                    FireEnemyGun(false,i);
                }

            #endregion

                #region PowerUps

            Powerups:

                InitiatePowerUps();

                if (_pu.PowerUpType == 0) goto BossRegion;     //No power up exist, skip this region

                ProcessPowerUpDirectionAndMove();
                
                //Draw
                sky.FillRectangle(_pu.PowerUpColour, new Rectangle(_pu.X, _pu.Y, _pu.Size, _pu.Size));

                //Reset poewr up after 25 bounces
                if (_pu.BounceCounter >= 25) _pu = new PowerUps();

                //Change blink
                _pu.Blink = !_pu.Blink;

                PowerUpMyJet();

                #endregion
                
                #region Boss
            
            BossRegion:

                #region BossType1

                if (!(_eB1.IsBossInitiated) && !(_eB2.IsBossInitiated)) goto Enemy_Bullet;            //Boss is not initated yet, skip this region

                if (_gc.BossType == 2) goto BossType2;                   //skip boss type 1  
            
                CreateBossObjectAndSortDirection(1);
                ProcessBossHpStatus(1);

                //Draw boss
                if (_eB1.CurrHp <= 0)   //boss has been destroyed
                {
                    DrawBossType1CommonParts(sky);
                    DrawBossType1ExpodingParts(sky);
                    BossType1PostExplosionProcess();
                }
                else                    //Boss is still there, draw accordingly
                {   
                    DrawBossType1MainFeselage(sky);
                    DrawBossType1CommonParts(sky);
                    DrawBossType1BackBurner(sky);
                }
                
                DestroyMyJetOnBossCollision(1);
                
                if(_eB1.CurrHp<=0) 
                    goto Enemy_Bullet;          //Boss is in destruction animation mode, skip this region

                FireGun_Boss(1);

                if(_gc.BossType==1) 
                    goto Enemy_Bullet;          //skip boss type 2

                #endregion

            BossType2:

                #region BossType2

                if (!_eB2.IsBossInitiated) goto BombExploded;            //Boss is not initated yet, skip this region

                CreateBossObjectAndSortDirection(2);
                ProcessBossHpStatus(2);

                //Draw boss
                if (_eB2.CurrHp <= 0)                   //boss has been destroyed
                {
                    DrawBossType2CommonParts(sky);
                    DrawBossType2ExpodingParts(sky);
                    BossType2PostExplosionProcess();
                }
                else                                    //Boss is still there
                {
                    BossType2BigGunMovementProcess();
                    DrawBossType2MainFeselage(sky);
                    DrawBossType2CommonParts(sky);
                    DrawBossType2BackBurner(sky);
                }
                
                DestroyMyJetOnBossCollision(2);

                if (_eB2.CurrHp <= 0) goto Enemy_Bullet;    //Boss is in destruction animation mode, skip this region

                FireGun_Boss(2);

                //Nose gun action
                if (!_eB2.NoseGunInitiated)
                {
                    if (_rand.Next(1, 400) == 45) _eB2.NoseGunInitiated = true;
                }
                
                //Charge gun
                if(_eB2.NoseGunInitiated) 
                    BossNoseCharge(sky);

                //fire the gun
                if (_eB2.NoseGunDelayCounter >= _eB2.NoseGunDelayMax && _eB2.NoseGunInitiated)
                    BossNoseGunFire();
                
                #endregion
            
                #endregion

                #region EnemyBullet Animation

            Enemy_Bullet:

                if (_ejb.Count == 0) goto ExplosionRegion; //no enemy bullet to process, skip this region

                for (int i = 0; i < _ejb.Count; i++)        //For every enemy bullet, process My jet damage
                {
                    DrawEnemyBulletAndProcessData(sky,i);
                    DamageMyJet(i);
                }

                #endregion

                #region Explosion Draw

            ExplosionRegion:

                //Draw explosions
                if (_exp.Count == 0) goto BombExploded; //nothing to explode, skip this region
                foreach (var explostions in _exp)
                {
                    sky.DrawPolygon(explostions.Colour, explostions.Points);
                    
                    explostions.VisibilityCounter++;
                    
                    if (explostions.VisibilityCounter > _gc.RegularExplosionVisibleTimeMax)
                        explostions.VisibilityCounter = 0;  //Mark exposion for removal from list
                }

                #endregion
            
                #region BombExploded

            BombExploded:
                if (!_mjb.BombXploded) goto Cleanup;    //No bomb to explode, skip this region

                _mjb.BombExplodedDisplayTimer++;

                if (_mjb.BombExplodedDisplayTimer == 1) //The moment bomb exploded..
                {
                    //Draw explosions for all the enemy jets and add scores for each jet
                    foreach (var enemy in _ej)
                    {
                        sky.FillPolygon(Brushes.Green, Explosion.Explode(enemy.RefX, enemy.RefY));
                        _gc.Score = _gc.Score + 10;
                    }

                    //Draw explosions for all the enemy helis and add scores for each jet
                    foreach (var enemy in _eH)
                    {
                        sky.FillPolygon(Brushes.Green, Explosion.Explode(enemy.RefX, enemy.RefY));
                        _gc.Score = _gc.Score + 15;
                    }
                    //Draw explosions for all the enemy bullets and add scores for each bullet
                    foreach (var enemyFire in _ejb)
                    {
                        sky.FillPolygon(Brushes.Green, Explosion.Explode(enemyFire.X, enemyFire.Y));
                        _gc.Score = _gc.Score + 1;
                    }
                    //do boss hp damage by 10% of full hp value
                    if (_eB1.IsBossInitiated) 
                    {
                        _eB1.CurrHp = _eB1.CurrHp - _gc.BossHp/10;
                    }

                    if (_eB2.IsBossInitiated)
                    {
                        _eB2.CurrHp = _eB2.CurrHp - _gc.BossHp / 10;
                    }
                }

                LblScore.Text = _gc.Score.ToString();

                if (_mjb.BombExplodedDisplayTimer > _gc.BombExplosionVisibleTimeMax)
                {
                    //Reset bomb exploded so rest can continue 
                    _mjb.BombXploded = false;
                    _mjb.BombExplodedDisplayTimer = 0;
                    
                    //Clear all enemy and bullet list
                    _ej.Clear();
                    _eH.Clear();
                    _ejb.Clear();
                }

                #endregion
               
            Cleanup:

                CleanUp();
                
            EndTasks:

                EndRoutine();
                LevelAndHpUpdate();
            }
        }
        
        //All private methods:
        
        #region BackGrounds
        private void InitiateMountain()
        {
            _go.MountainDrawInitiated = true;
            _go.Height = _rand.Next(20, 150);     //Get a random number to control mountain Height
            _go.Width = _rand.Next(250, 300);

            //Start from far right
            _go.RefX = PbConsole.Width;
            _go.RefY = PbConsole.Height;

            switch (_rand.Next(1, 3))             //select random colour from three choices as mountain colour
            {
                case 1: { _go.MountainColor = Brushes.Green; break; }
                case 2: { _go.MountainColor = Brushes.Yellow; break; }
                case 3: { _go.MountainColor = Brushes.SaddleBrown; break; }
            }
        }
        private void InitiateCloud()
        {
            _so = new SkyObjects();
            _so.Width = (byte)_rand.Next(100, 200);                          //Get random width for cloud
            _so.Height = (byte)_rand.Next(30, 90);                         //Get random height for cloud
            _so.RefX = PbConsole.Width;                                     //Start from far right
            _so.RefY = _rand.Next(5, PbConsole.Height / 2 - _so.Height);       //Start somewhere between top half screen

            switch (_rand.Next(1, 8))                                         //Get colour randomly from four choices
            {
                case 1:
                case 4: { _so.FillColor = Brushes.PowderBlue; break; }
                case 2:
                case 5: { _so.FillColor = Brushes.DarkGray; break; }
                case 3:
                case 6: { _so.FillColor = Brushes.LightGray; break; }
                default: _so.FillColor = Brushes.White; break;
            }

            _so.DeltaX = (byte)_rand.Next(1, 3);                            //Assign random speed for cloud (1 being slowest/ 3 being highest)

            if (_rand.Next(1, 10) == 5) _so.HasBorder = true;                //Randomly decide whether to apply border

            //Add the cloud object in list.
            _cL.Add(new CloudFactory { CloudInitiated = true, RefX = _so.RefX, FillColor = _so.FillColor, DeltaX = _so.DeltaX, Width = _so.Width, ShapePoints = _so.CloudPoints(), IsGenerated = false, HasBorder = _so.HasBorder });
            
        }
        private void DrawCloudAndProcessData(Graphics sky)
        {
            foreach (var cloud in _cL)
            {
                if (cloud.CloudInitiated)
                {
                    sky.FillClosedCurve(cloud.FillColor, cloud.ProcessedShapePoints());                                  //Draw cloud

                    if (cloud.HasBorder) sky.DrawClosedCurve(new Pen(Color.DimGray), cloud.ProcessedShapePoints());      //Draw border

                    cloud.ShapePoints = cloud.ProcessedShapePoints();                                                   //Update shapepoints with current shapepoints
                }

                cloud.RefX = cloud.RefX - cloud.DeltaX;

                if (cloud.RefX + cloud.Width < 0) cloud.CloudInitiated = false;                      //If the whole cloud left windown, mark it for removal
                if (cloud.RefX + (cloud.Width * _gc.CloudGenerationOdd / 100) <= PbConsole.Width) cloud.IsGenerated = true;   //If cloud shifted on left enough to be half drawn, mark it (to allow next cloud generation)
            }
        }
        #endregion

        #region My Jet
        private void FireGun_MyJet()
        {
            switch (_mj.CurrentActiveGun)   //Add bullets in bullet list based on active gun number
            {
                case 1:
                    {
                        _mjgb.Add(new MyJetGunBullet { GunFireStart = true, X = _mj.X2, Y = _mj.Y2 - 10, Size = _gc.MyJetBulletSize });
                        break;
                    }
                case 2:
                    {
                        _mjgb.Add(new MyJetGunBullet { GunFireStart = true, X = _mj.X2, Y = _mj.Y2 - 10, Size = 3 });
                        _mjgb.Add(new MyJetGunBullet { GunFireStart = true, X = _mj.X2, Y = _mj.Y2 - 13, Size = 3, DeltaY = -1 });   //DeltaY - 1 so bullet will move upwards
                        break;
                    }
                case 3:
                    {
                        _mjgb.Add(new MyJetGunBullet { GunFireStart = true, X = _mj.X2, Y = _mj.Y2 - 10, Size = 3 });
                        _mjgb.Add(new MyJetGunBullet { GunFireStart = true, X = _mj.X2, Y = _mj.Y2 - 13, Size = 3, DeltaY = -1 });
                        _mjgb.Add(new MyJetGunBullet { GunFireStart = true, X = _mj.X2, Y = _mj.Y2 - 7, Size = 3, DeltaY = 1 });        //DeltaY + 1 so bullet will move downwards
                        break;
                    }
            }

            PlaySound(Gunfire);
            
            _ki.KeyFire = false;
        }
        private void BombDrop_MyJet(Graphics sky)
        {
            //jet drop bomb - only when bomb remains and no other bomb is being dropped
            if (_ki.KeyBomb && _mjb.BombRemains > 0 && _mjb.BombX == 0)
            {
                _mjb.BombX = _mj.X2;
                _mjb.BombY = _mj.Y2;
                _mjb.BombRemains--;
            }

            //Draw bomb drop and post draw process
            if (_mjb.BombX > 0) //Only when bomb is away
            {
                PlaySound(BombAway); //Bomb away sound
                sky.FillEllipse(Brushes.Orange, new Rectangle { Height = 5, Width = 5, X = _mjb.BombX, Y = _mjb.BombY });

                _mjb.BombY = _mjb.BombY + _gc.MyJetBombDelta;       //move bomb down for next round
                if (_mjb.BombY > PbConsole.Height)                  //If bomb touched the ground, explode it
                    ExplodeBomb();
            }
        }
        private void GetMyJetCurrentDirection()
        {
            //jet horizontal direction
            if (_ki.KeyDirectionHorizontal > 0)
            {
                if (_ki.KeyDirectionHorizontal == 39)
                    _mj.RefX = _mj.RefX + _gc.MyJetDelta;

                if (_ki.KeyDirectionHorizontal == 37)
                    _mj.RefX = _mj.RefX - _gc.MyJetDelta;
            }

            if (_ki.KeyDirectionVertical > 0)
            {
                //jet vertical direction
                if (_ki.KeyDirectionVertical == 40)
                {
                    _mj.RefY = _mj.RefY + _gc.MyJetDelta;
                    _mj.MovementState = -1;                     //Set state will draw jet based for going up state
                }
                if (_ki.KeyDirectionVertical == 38)
                {
                    _mj.RefY = _mj.RefY - _gc.MyJetDelta;
                    _mj.MovementState = 1;                      //Set state to dictate jet for going down state
                }
            }
            else
            {
                _mj.MovementState = 0;                          //Default state - jet is vertically stationery
            }
        }
        private void KeepMyJetWithinCanvas()
        {
            //Keep the jet within canvas
            if (_mj.RefY < 0)
                _mj.RefY = 0;
            if (_mj.Y2 > PbConsole.Height)
                _mj.RefY = PbConsole.Height - (_mj.Y2 - _mj.RefY);
            if (_mj.RefX < 0)
                _mj.RefX = 0;
            if (_mj.X2 > PbConsole.Width)
                _mj.RefX = PbConsole.Width - (_mj.X2 - _mj.RefX);
        }
        private void DrawMyJet(Graphics sky)
        {
            //draw jet body
            switch (_mj.MovementState)
            {
                case 0:
                    {
                        sky.FillPolygon(Brushes.Gold, _mj.Fuselage());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.WingPosOne());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.TailPosOne());
                        break;
                    }
                case 1:
                    {
                        sky.FillPolygon(Brushes.Gold, _mj.Fuselage());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.LefWingPosTwo());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.RightWingPosTwo());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.TailPosTwo());
                        break;
                    }
                case -1:
                    {
                        sky.FillPolygon(Brushes.Gold, _mj.Fuselage());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.LefWingPosThree());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.RightWingPosThree());
                        sky.FillPolygon(Brushes.DodgerBlue, _mj.TailPosThree());
                        break;
                    }
            }

            sky.DrawPolygon(new Pen(Color.Gray), _mj.Fuselage());

            //draw jetexhaust burner
            if (_mj.JetBurnerController % 3 == 0) //every after 3 timer.ticks
            {
                sky.FillPolygon(Brushes.Orange, _mj.JetBurner());
            }
            else
            {
                sky.FillPolygon(Brushes.Red, _mj.JetBurner());
            }

            _mj.JetBurnerController++;
            if (_mj.JetBurnerController > 5)
                _mj.JetBurnerController = 0;
        }
        private void DrawMyJetBulletAndProcessData(Graphics sky, int bulletIndex)
        {
            //Draw bullet
            if (_dayPhase == 3)
            {
                sky.DrawEllipse(new Pen(Color.White), _mjgb[bulletIndex].X, _mjgb[bulletIndex].Y, _mjgb[bulletIndex].Size, _mjgb[bulletIndex].Size);
            }
            else
            {
                sky.DrawEllipse(_myJetBulletColor, _mjgb[bulletIndex].X, _mjgb[bulletIndex].Y, _mjgb[bulletIndex].Size, _mjgb[bulletIndex].Size);
            }

            //Move bullet for next turn
            _mjgb[bulletIndex].X = _mjgb[bulletIndex].X + 10;
            _mjgb[bulletIndex].Y = _mjgb[bulletIndex].Y + _mjgb[bulletIndex].DeltaY;

            //Identy bullets out of window and mark for removal from list
            if (_mjgb[bulletIndex].X > PbConsole.Width || _mjgb[bulletIndex].Y < 0 || _mjgb[bulletIndex].Y > PbConsole.Height)
            {
                _mjgb[bulletIndex].GunFireStart = false;
            }
        }
        private void AttackEnemyJet(int bulletIndex)
        {
            //attack jet
            for (int j = 0; j < _ej.Count; j++)
            {
                if (_mjgb[bulletIndex].X + 10 >= _ej[j].RefX && _mjgb[bulletIndex].X <= _ej[j].X2 &&
                    ((_mjgb[bulletIndex].Y >= _ej[j].RefY && _mjgb[bulletIndex].Y <= _ej[j].Y2) ||
                     (_mjgb[bulletIndex].Y + _mjgb[bulletIndex].Size >= _ej[j].RefY && _mjgb[bulletIndex].Y + _mjgb[bulletIndex].Size <= _ej[j].Y2)))
                {
                    EnemyDestroyed(_mjgb[bulletIndex].X, _mjgb[bulletIndex].Y, false);              //Add an explosion in the list
                    _mjgb[bulletIndex].GunFireStart = false;                              //Mark the bullet for removal from list
                    _ej[j].Hp--;
                    if (_ej[j].Hp <= 0) _ej[j].MakeRedundant = true;            //Mark the jet for removal from list

                    //play enemy destroy sound
                    PlaySound(EnemyXplode);
                }
            }
        }
        private void AttackEnemyHeli(int bulletIndex)
        {
            //attack heli
            for (int k = 0; k < _eH.Count; k++)
            {
                if (_mjgb[bulletIndex].X + 10 >= _eH[k].RefX && _mjgb[bulletIndex].X <= _eH[k].X2 &&
                    ((_mjgb[bulletIndex].Y >= _eH[k].RefY && _mjgb[bulletIndex].Y <= _eH[k].Y2) ||
                     (_mjgb[bulletIndex].Y + _mjgb[bulletIndex].Size >= _eH[k].RefY && _mjgb[bulletIndex].Y + _mjgb[bulletIndex].Size <= _eH[k].Y2)))
                {
                    EnemyDestroyed(_mjgb[bulletIndex].X, _mjgb[bulletIndex].Y, false);                //Add an explosion in the list
                    _mjgb[bulletIndex].GunFireStart = false;                               //Mark the bullet for removal from list
                    _eH[k].Hp--;
                    if (_eH[k].Hp <= 0) _eH[k].MakeRedundant = true;            //Mark the jet for removal from list

                    //play enemy destroy sound
                    PlaySound(EnemyXplode);
                }
            }
        }
        private void AttackEnemyBossType1(int bulletIndex)
        {
            if (_mjgb[bulletIndex].X + 10 >= _eB1.RefX && _mjgb[bulletIndex].X <= _eB1.X2 &&
                                ((_mjgb[bulletIndex].Y >= _eB1.HitY1 && _mjgb[bulletIndex].Y <= _eB1.HitY2) || (_mjgb[bulletIndex].Y + 10 >= _eB1.HitY1 && _mjgb[bulletIndex].Y + 10 <= _eB1.HitY2)))
            {
                _eB1.CurrHp--;
                EnemyDestroyed(_mjgb[bulletIndex].X, _mjgb[bulletIndex].Y, true);                 //Add an exposion in the list
                _mjgb[bulletIndex].GunFireStart = false;                               //Mark the bullet for removal from list
            }
        }
        private void AttackEnemyBossType2(int bulletIndex)
        {
            if (
                (_mjgb[bulletIndex].X + 10 >= _eB2.DamageAreaCT.X && _mjgb[bulletIndex].X <= _eB2.DamageAreaCB.X &&
                ((_mjgb[bulletIndex].Y >= _eB2.DamageAreaCT.Y && _mjgb[bulletIndex].Y <= _eB2.DamageAreaCB.Y) || (_mjgb[bulletIndex].Y + 10 >= _eB2.DamageAreaCT.Y && _mjgb[bulletIndex].Y + 10 <= _eB2.DamageAreaCB.Y)))
                ||
                (_mjgb[bulletIndex].X + 10 >= _eB2.DamageAreaTT.X && _mjgb[bulletIndex].X <= _eB2.DamageAreaTB.X &&
                ((_mjgb[bulletIndex].Y >= _eB2.DamageAreaTT.Y && _mjgb[bulletIndex].Y <= _eB2.DamageAreaTB.Y) || (_mjgb[bulletIndex].Y + 10 >= _eB2.DamageAreaTT.Y && _mjgb[bulletIndex].Y + 10 <= _eB2.DamageAreaTB.Y)))
                ||
                (_mjgb[bulletIndex].X + 10 >= _eB2.DamageAreaCT.X && _mjgb[bulletIndex].X <= _eB2.DamageAreaCB.X &&
                ((_mjgb[bulletIndex].Y >= _eB2.DamageAreaBT.Y && _mjgb[bulletIndex].Y <= _eB2.DamageAreaBB.Y) || (_mjgb[bulletIndex].Y + 10 >= _eB2.DamageAreaBT.Y && _mjgb[bulletIndex].Y + 10 <= _eB2.DamageAreaBB.Y)))
               )
            {
                _eB2.CurrHp--;
                EnemyDestroyed(_mjgb[bulletIndex].X, _mjgb[bulletIndex].Y, true);                 //Add an exposion in the list
                _mjgb[bulletIndex].GunFireStart = false;                               //Mark the bullet for removal from list
            }
        }
        private void ExplodeBomb()
        {
            PlaySound(BombXplode);

            _mjb.BombXploded = true; //This will freeze all the actions except bomb explosions from next round till set time.
            _mjb.BombX = 0;
            _mjb.BombY = 0;
        }
        #endregion

        #region Enemy Jets
        private void InitiateEnemyJets()
        {
            //Initiate enemy                    
            if (_ej.Count < (_eB1.IsBossInitiated||_eB2.IsBossInitiated ? 1 : _gc.MaxEnemyJetT1PerLvl))                 //Only when current enemy cound is less than set limit        
            {
                if (_rand.Next(1, _gc.EnemyJetT1GenOdd) == 12)     //generate enemy object based on random selection and add in enemy list
                {
                    _ej.Add(new SmallJet { MovementState = 0, RefX = PbConsole.Width, RefY = _rand.Next(5, PbConsole.Height), Hp = _gc.EnemyJetT1Hp, MovementDelta = _gc.EnemyJetT1MovementDelta });
                }
            }

            //Special enemies from higher level

            if (_gc.LevelCompleted > 0 && _rand.Next(0, _gc.EnemyJetT2GenOdd) == 10 && _ej.Select(x => x).Count(x => x.JetType == 2) < (_eB1.IsBossInitiated ? 1 : _gc.MaxEnemyJetT2PerLvl))
            {
                _ej.Add(new SmallJet { MovementState = 0, RefX = PbConsole.Width, RefY = _rand.Next(5, PbConsole.Height), JetType = 2, Hp = _gc.EnemyJetT2Hp, MovementDelta = _gc.EnemyJetT2MovementDelta });
            }
        }
        private void ProcessEnemyJetDirectionAndMovement(int enemyJetIndex)
        {
            //Select direction state randomly
            switch (_rand.Next(1, 15))
            {
                case 1: { _ej[enemyJetIndex].Direction = 1; break; } //Down
                case 2: { _ej[enemyJetIndex].Direction = -1; break; } //Up
                case 3: { _ej[enemyJetIndex].Direction = 0; break; } //Back

                default: _ej[enemyJetIndex].Direction = _ej[enemyJetIndex].Direction; break;
            }

            //Keep enemy within canvas
            if (_ej[enemyJetIndex].RefY < 0) _ej[enemyJetIndex].Direction = 1;

            if (_ej[enemyJetIndex].Y2 > PbConsole.Height) _ej[enemyJetIndex].Direction = -1;

            //Move enemy according to Direction
            switch (_ej[enemyJetIndex].Direction)
            {
                case 0: { _ej[enemyJetIndex].RefX = _ej[enemyJetIndex].RefX + _ej[enemyJetIndex].MovementDelta * 2; break; }
                case 1: { _ej[enemyJetIndex].RefY = _ej[enemyJetIndex].RefY + _ej[enemyJetIndex].MovementDelta; break; }
                case -1: { _ej[enemyJetIndex].RefY = _ej[enemyJetIndex].RefY - _ej[enemyJetIndex].MovementDelta; break; }
            }

            //Continuous Move toward left
            _ej[enemyJetIndex].RefX = _ej[enemyJetIndex].RefX - _ej[enemyJetIndex].MovementDelta;

            //get rid of enemy which are out of window
            if (_ej[enemyJetIndex].X2 < 0) _ej[enemyJetIndex].MakeRedundant = true;
        }
        private void DrawEnemyJet(Graphics sky, int enemyJetIndex)
        {
            sky.FillPolygon(_ej[enemyJetIndex].JetType == 1 ? Brushes.Tomato : Brushes.DarkSlateGray, _ej[enemyJetIndex].FuselagePosOne());
            sky.FillPolygon(_ej[enemyJetIndex].JetType == 1 ? Brushes.DarkSlateGray : Brushes.Salmon, _ej[enemyJetIndex].WingTopPosOne());
            sky.FillPolygon(_ej[enemyJetIndex].JetType == 1 ? Brushes.DarkSlateGray : Brushes.Salmon, _ej[enemyJetIndex].WingBottomPosOne());

        }
        private void EnemyJetAndMyJetCollision(int enemyJetIndex)
        {
            //damage my jet & enemy jet on collision (when godmode is false)
            if (!_gc.GodMode)
            {
                if (_ej[enemyJetIndex].RefX <= _mj.X2 && _ej[enemyJetIndex].X2 >= _mj.RefX
                    && ((_ej[enemyJetIndex].RefY >= _mj.RefY && _ej[enemyJetIndex].RefY <= _mj.Y2) ||
                        (_ej[enemyJetIndex].Y2 >= _mj.RefY && _ej[enemyJetIndex].Y2 <= _mj.Y2)))
                {
                    _ej[enemyJetIndex].MakeRedundant = true;                                //Mark enemy jet for removal from list
                    _exp.Add(new Explosion
                    {
                        Colour = new Pen(Color.Red),
                        VisibilityCounter = 1,
                        Points = Explosion.Explode(_mj.RefX, _mj.RefY)
                    });
                    _exp.Add(new Explosion
                    {
                        Colour = new Pen(Color.Green),
                        VisibilityCounter = 1,
                        Points = Explosion.Explode(_ej[enemyJetIndex].X2, _ej[enemyJetIndex].Y2)
                    });

                    //Play sound for enemy destroy
                    PlaySound(EnemyXplode);

                    _mj.Hp--;                                                   //Reduce my jet's hp by 1

                    if (_mj.Hp <= 0) GameOver();                                   //If my hp reaches 0, game over
                }
            }
        }
        #endregion

        #region Enemy Helis
        private void InitiateEnemyHeli()
        {
            //Initiate enemy                    
            if (_eH.Count < (_eB1.IsBossInitiated||_eB2.IsBossInitiated ? 1 : _gc.MaxEnemyHeliT1PerLvl))                 //Only when current enemy cound is less than set limit        
            {
                if (_rand.Next(1, _gc.EnemyHeliT1GenOdd) == 5)         //generate enemy object based on random selection and add in enemy list
                {
                    _eH.Add(new SmallHeli { RotorAtFront = true, RefX = PbConsole.Width, RefY = _rand.Next(1, PbConsole.Height), Hp = _gc.EnemyHeliT1Hp, MovmementDelta = _gc.EnemyHeliT1MovementDelta });   //*1 is for type 1
                }
            }
        }
        private void ProcessEnemyHeliDirection(int i)
        {
            if (!_eH[i].InitiateMove)
            {
                //Select direction state randomly, heli is statinoery till direction is selected. Only move forward till the heli is fully in the canvas

                switch (_rand.Next(1, 200))
                {
                    case 1:
                        {
                            _eH[i].Direction = (sbyte)(_eH[i].X2 < PbConsole.Width ? 1 : 4);
                            _eH[i].InitiateMove = true;
                            break;
                        } //Down
                    case 10:
                        {
                            _eH[i].Direction = (sbyte)(_eH[i].X2 < PbConsole.Width ? -1 : 4);
                            _eH[i].InitiateMove = true;
                            break;
                        } //Up
                    case 20:
                        {
                            _eH[i].Direction = (sbyte)(_eH[i].X2 < PbConsole.Width ? 0 : 4);
                            _eH[i].InitiateMove = true;
                            break;
                        } //Back
                    case 5:
                    case 15:
                    case 25:
                        {
                            _eH[i].Direction = 4;
                            _eH[i].InitiateMove = true;
                            break;
                        } //Forward
                    default:
                        _eH[i].Direction = 4;
                        break;
                }
            }
        }
        private void ProcessEnemyHeliMovement(int enemyHeliIndex)
        {
            if (_eH[enemyHeliIndex].InitiateMove)
            {
                //Move enemy according to Direction, no further selection while moving towards a single direction (except bounce)
                switch (_eH[enemyHeliIndex].Direction)
                {
                    case 0:
                        {
                            _eH[enemyHeliIndex].RefX = _eH[enemyHeliIndex].RefX + _eH[enemyHeliIndex].MovmementDelta;
                            break;
                        }
                    case 1:
                        {
                            _eH[enemyHeliIndex].RefY = _eH[enemyHeliIndex].RefY + _eH[enemyHeliIndex].MovmementDelta;
                            break;
                        }
                    case -1:
                        {
                            _eH[enemyHeliIndex].RefY = _eH[enemyHeliIndex].RefY - _eH[enemyHeliIndex].MovmementDelta;
                            break;
                        }
                    case 4:
                        {
                            _eH[enemyHeliIndex].RefX = _eH[enemyHeliIndex].RefX - _eH[enemyHeliIndex].MovmementDelta;
                            break;
                        }
                }

                //Keep enemy within canvas
                if (_eH[enemyHeliIndex].RefY < 0) _eH[enemyHeliIndex].Direction = 1;

                if (_eH[enemyHeliIndex].Y2 > PbConsole.Height) _eH[enemyHeliIndex].Direction = -1;

                //get rid of enemy which are out of window
                if (_eH[enemyHeliIndex].X2 < 0) _eH[enemyHeliIndex].MakeRedundant = true;

                //Stall movement counter process and reset
                _eH[enemyHeliIndex].StallMovementCounter++;

                if (_eH[enemyHeliIndex].StallMovementCounter > 50)
                {
                    _eH[enemyHeliIndex].StallMovementCounter = 0;
                    _eH[enemyHeliIndex].InitiateMove = false;
                    _eH[enemyHeliIndex].Direction = 4;
                }
            }

            //Update rotor movement state to apply drawing shape accordingly
            _eH[enemyHeliIndex].RotorAtFront = !_eH[enemyHeliIndex].RotorAtFront;
        }
        private void DrawEnemyHeli(Graphics sky, int enemyHeliIndex)
        {
            //Draw enemy
            sky.FillPolygon(Brushes.Goldenrod, _eH[enemyHeliIndex].FuselageMain());
            sky.FillPolygon(Brushes.CadetBlue, _eH[enemyHeliIndex].FuselageWithTail());
            sky.FillPolygon(Brushes.Black, _eH[enemyHeliIndex].Cockpit());

            if (_eH[enemyHeliIndex].RotorAtFront)
            {
                sky.FillPolygon(Brushes.DarkCyan, _eH[enemyHeliIndex].RotorLeft());
            }
            else
            {
                sky.FillPolygon(Brushes.DarkCyan, _eH[enemyHeliIndex].RotorRight());
            }
        }
        private void EnemyHeliAndMyJetCollision(int enemyHeliIndex)
        {
            //destroy my jet & enemy jet on collision (when godmode is false)
            if (!_gc.GodMode)
            {
                if (_eH[enemyHeliIndex].X2 >= _mj.X2 && _eH[enemyHeliIndex].RefX <= _mj.RefX
                    && ((_eH[enemyHeliIndex].RefY >= _mj.RefY && _eH[enemyHeliIndex].RefY <= _mj.Y2) ||
                        (_eH[enemyHeliIndex].Y2 >= _mj.RefY && _eH[enemyHeliIndex].Y2 <= _mj.Y2)))
                {
                    _eH[enemyHeliIndex].MakeRedundant = true;                                //Mark enemy jet for removal from list
                    _exp.Add(new Explosion
                    {
                        Colour = new Pen(Color.Red),
                        VisibilityCounter = 1,
                        Points = Explosion.Explode(_mj.RefX, _mj.RefY)
                    });
                    _exp.Add(new Explosion
                    {
                        Colour = new Pen(Color.Green),
                        VisibilityCounter = 1,
                        Points = Explosion.Explode(_eH[enemyHeliIndex].X2, _eH[enemyHeliIndex].Y2)
                    });

                    //Play sound for enemy explosion
                    PlaySound(EnemyXplode);

                    _mj.Hp--;                                                   //Reduce my jet's hp by 1

                    if (_mj.Hp <= 0) GameOver();                                //If my hp reaches 0, game over
                }
            }
        }
        #endregion

        #region Enemy Shared
        private void FireEnemyGun(bool isJet, int enemyIndex)
        {
            if (isJet)
            {
                //Fire enemy gun - Based on random number selection, add bullet object in enemy bullet list.
                if (_rand.Next(1, _gc.EnemyBulletGenOdd) == 15 && _ejb.Count <= _gc.SmallEnemyBulletMax)
                    _ejb.Add(new EnemyBullet
                    {
                        GunFireStart = true,
                        Size = _gc.EnemySmallJetBulletSize,
                        X = _ej[enemyIndex].RefX,
                        Y = _ej[enemyIndex].Y2 - _ej[enemyIndex].Height/2,
                        Damage = 1,
                        DeltaX = _gc.EnemyJetT1BulletDelta,
                        Colour = Brushes.Red
                    });
            }
            else
            {
                //Fire enemy gun - Based on random number selection, add bullet object in enemy bullet list.
                if (_rand.Next(1, _gc.EnemyBulletGenOdd) == 15 && _ejb.Count <= _gc.SmallEnemyBulletMax)
                {
                    for (int r = 0; r < _gc.EnemyHeliT1BulletPerRound; r++)
                    {
                        var heliBullRound = new EnemyBullet { GunFireStart = true, X = _eH[enemyIndex].RefX - 2, Y = _eH[enemyIndex].RefY + _eH[enemyIndex].Height / 2, InitialX = _eH[enemyIndex].RefX, InitialY = _eH[enemyIndex].RefY + _eH[enemyIndex].Height / 2, MyJetX = _mj.X2, MyJetY = _mj.RefY, DeltaX = _gc.EnemyHeliT1BulletDelta, Size = _gc.EnemyHeliT1BulletSize, Colour = Brushes.OrangeRed, Damage = 1 };
                        heliBullRound.X = heliBullRound.X - heliBullRound.Size * (_gc.EnemyHeliT1BulletPerRound - r);
                        heliBullRound.Y = (int)(heliBullRound.Y + heliBullRound.DeltaGunTargetY * (_gc.EnemyHeliT1BulletPerRound - r));
                        _ejb.Add(heliBullRound);

                        //Play sound for heli gunfire
                        PlaySound(Gunfire);
                    }
                }
            }
        }
        private void EnemyDestroyed(int x, int y, bool isBoss)
        {
            if (!isBoss) _gc.Score = _gc.Score + 10;

            LblScore.Text = _gc.Score.ToString();
            _exp.Add(new Explosion { Colour = new Pen(Color.Green), VisibilityCounter = 1, Points = Explosion.Explode(x, y) });
        }
        private void DrawEnemyBulletAndProcessData(Graphics sky, int enemyBulletIndex)
        {
            if (_ejb[enemyBulletIndex].IsHollowBullet)
            {
                sky.DrawEllipse(_ejb[enemyBulletIndex].ColourPen, new Rectangle { Height = _ejb[enemyBulletIndex].Size, Width = _ejb[enemyBulletIndex].Size, X = _ejb[enemyBulletIndex].X, Y = _ejb[enemyBulletIndex].Y });
            }
            else
            {
                sky.FillEllipse(_ejb[enemyBulletIndex].Colour, new Rectangle { Height = _ejb[enemyBulletIndex].Size, Width = _ejb[enemyBulletIndex].Size, X = _ejb[enemyBulletIndex].X, Y = _ejb[enemyBulletIndex].Y });
            }

            _ejb[enemyBulletIndex].X = _ejb[enemyBulletIndex].X - _ejb[enemyBulletIndex].DeltaX;
            _ejb[enemyBulletIndex].Y = (int)(_ejb[enemyBulletIndex].Y + _ejb[enemyBulletIndex].DeltaGunTargetY);

            if (_ejb[enemyBulletIndex].X < 0 || _ejb[enemyBulletIndex].Y < 0 || _ejb[enemyBulletIndex].Y > PbConsole.Height || _ejb[enemyBulletIndex].X > PbConsole.Width) _ejb[enemyBulletIndex].GunFireStart = false;
        }
        private void DamageMyJet(int enemyBulletIndex)
        {
            if (!_gc.GodMode)
            {
                if (_ejb[enemyBulletIndex].X <= _mj.X2 && (_ejb[enemyBulletIndex].X + _ejb[enemyBulletIndex].Size) >= _mj.RefX &&
                   ((_ejb[enemyBulletIndex].Y >= _mj.RefY && _ejb[enemyBulletIndex].Y <= _mj.Y2) ||
                   (_ejb[enemyBulletIndex].Y + _ejb[enemyBulletIndex].Size >= _mj.RefY && _ejb[enemyBulletIndex].Y + _ejb[enemyBulletIndex].Size <= _mj.Y2)))
                {
                    _exp.Add(new Explosion { Colour = new Pen(Color.Red), VisibilityCounter = 1, Points = Explosion.Explode(_ejb[enemyBulletIndex].X, _ejb[enemyBulletIndex].Y) });   //Add new explosion in Red (because it's my jet) in list

                    _mj.Hp = (sbyte)(_mj.Hp - _ejb[enemyBulletIndex].Damage);                           //Reduce my jet's hp by 1;                        

                    _ejb[enemyBulletIndex].GunFireStart = false;       //Mark enemy bullet for removal from the list
                    if (_mj.Hp <= 0) GameOver();        //If my jet's hp reaches 0, game over
                }
            }
        }
        #endregion

        #region Power ups
        private void InitiatePowerUps()
        {
            if (_pu.PowerUpType == 0 || _pu.PowerUpType > 3)
            {
                //initiate
                _pu.PowerUpType = _rand.Next(1, _gc.PowerUpGenOdd);

                //Reset power up type if max reached
                if ((_mj.CurrentActiveGun == 3 && _pu.PowerUpType == 3) ||
                    (_mj.CurrentBulletLimit == 10 && _pu.PowerUpType == 2) ||
                    (_mjb.BombRemains == 5 && _pu.PowerUpType == 1))
                    _pu.PowerUpType = 0;

                if (_pu.PowerUpType > 3)
                {
                    _pu.PowerUpType = 0;
                }
                else
                {
                    if (_rand.Next(1, 3000) == 1500)                             //special power
                        _pu.IsFullPower = true;
                    _pu.X = PbConsole.Width;                                    //Start on the far right
                    _pu.Y = _rand.Next(1, PbConsole.Height);                     //Anywhere between top and bottom
                    if (_pu.Y > PbConsole.Height / 2) _pu.IsTopToBottom = false;  //Set initial direction based on Y value
                }
            }
        }
        private void ProcessPowerUpDirectionAndMove()
        {
            //Boundary bounce direction
            if (_pu.Y < 0)                              //Hit top wall, change direction to bottom
            {
                _pu.IsTopToBottom = true;
                _pu.BounceCounter++;
            }
            if (_pu.Y + _pu.Size > PbConsole.Height)    //Hit bottom wall, change direction to top
            {
                _pu.IsTopToBottom = false;
                _pu.BounceCounter++;
            }
            if (_pu.X < 0)                              //Hit left wall, change direction to right
            {
                _pu.IsRightToLeft = false;
                _pu.BounceCounter++;
            }
            if (_pu.X + _pu.Size > PbConsole.Width)     //Hit right wall, change direction to left
            {
                _pu.IsRightToLeft = true;
                _pu.BounceCounter++;
            }

            //Progress move based on direction
            _pu.X = _pu.IsRightToLeft ? _pu.X - _pu.DeltaX : _pu.X + _pu.DeltaX;
            _pu.Y = _pu.IsTopToBottom ? _pu.Y + _pu.DeltaY : _pu.Y - _pu.DeltaY;
        }
        private void PowerUpMyJet()
        {
            //PowerUp my jet on contact
            if (_mj.X2 >= _pu.X && _mj.RefX <= _pu.X + _pu.Size &&
                ((_mj.RefY >= _pu.Y && _mj.RefY <= _pu.Y + _pu.Size) ||
                 (_mj.Y2 >= _pu.Y && _mj.Y2 <= _pu.Y + _pu.Size)))
            {

                if (_pu.IsFullPower)
                {
                    _mjb.BombRemains = 5;
                    _mj.CurrentActiveGun = 3;
                    _mj.CurrentBulletLimit = 10;
                    _mj.Hp = (sbyte)(_mj.Hp < 10 ? 10 : _mj.Hp);
                }
                else
                {
                    switch (_pu.PowerUpType)
                    {
                        case 1:
                            {
                                _mjb.BombRemains++;
                                break;
                            }
                        case 2:
                            {
                                _mj.CurrentBulletLimit++;
                                break;
                            }
                        case 3:
                            {
                                _mj.CurrentActiveGun++;
                                break;
                            }
                    }
                }
                //play power up sound
                PlaySound(PowerUp);

                //reset powerup object for next one
                _pu = new PowerUps();
            }
        }
        #endregion

        #region Boss Shared
        private void BossFight()
        {
            if (_gc.BossType == 1)
            {
                _eB1.IsBossInitiated = true; //This will allow boss fight animation from next round till the boss is destroyed
            }
            else
            {
                _eB2.IsBossInitiated = true;
            }

            LblBoss.Visible = true;
            LblBossRem.Visible = true;
            BackColor = Color.Red;
        }
        private void CreateBossObjectAndSortDirection(byte bossType)
        {
            if (bossType == 1)
            {
                //Create boss object
                if (_eB1.RefX == 0 && _eB1.RefY == 0)
                {
                    _eB1.RefX = PbConsole.Width; //start all the way to right
                    _eB1.RefY = _rand.Next(0, PbConsole.Height); //Start anywhere in between top and bottom
                }
                _eB1.PrevDirection = _eB1.Direction; //Get previous direction
                _eB1.Direction = (byte) _rand.Next(0, 15); //Get a random direction 

                if (_eB1.Direction > 4 || _eB1.Direction < 1)
                    _eB1.Direction = _eB1.PrevDirection;
                //Re-instate previous direction of new direction is not between 1 to 4

                //boundary bounce 
                if (_eB1.RefY < 0) _eB1.Direction = 3;
                if (_eB1.RefY + _eB1.Height > PbConsole.Height) _eB1.Direction = 1;
                if (_eB1.RefX < 0) _eB1.Direction = 4;
                if (_eB1.Height > PbConsole.Height) _eB1.Direction = 2;

                switch (_eB1.Direction)
                {
                    case 1:
                    {
                        _eB1.RefY = _eB1.RefY - _eB1.MovementDelta;
                        break;
                    } //Move up
                    case 2:
                    {
                        _eB1.RefX = _eB1.RefX - _eB1.MovementDelta;
                        break;
                    } //Move left
                    case 3:
                    {
                        _eB1.RefY = _eB1.RefY + _eB1.MovementDelta;
                        break;
                    } //Move down
                    case 4:
                    {
                        _eB1.RefX = _eB1.RefX + _eB1.MovementDelta;
                        break;
                    } //Move right
                }

                //move to left till the whole boss appears in screen
                if (_eB1.X2 > PbConsole.Width)
                    _eB1.RefX = _eB1.RefX - 3;

                //toggle control burner field for animation
                _eB1.ToggleBurner = !_eB1.ToggleBurner;
            }
            else
            {
                //Create boss object
                if (_eB2.RefX == 0 && _eB2.RefY == 0)
                {
                    _eB2.RefX = PbConsole.Width; //start all the way to right
                    _eB2.RefY = _rand.Next(0, PbConsole.Height); //Start anywhere in between top and bottom
                }
                _eB2.PrevDirection = _eB2.Direction; //Get previous direction

                if (_eB2.NoseGunInitiated) //Keep boss stationery
                {
                    _eB2.Direction = 0;
                }
                else
                {
                    _eB2.Direction = (byte) _rand.Next(0, 100); //Get a random direction 

                    if (_eB2.Direction > 4 || _eB2.Direction < 1) //Re-instate previous direction of new direction is not between 1 to 4
                        _eB2.Direction = _eB2.PrevDirection;
                }
                //boundary bounce 
                if (_eB2.RefY < 0) _eB2.Direction = 3;
                if (_eB2.RefY + _eB2.Height > PbConsole.Height) _eB2.Direction = 1;
                if (_eB2.RefX < 0) _eB2.Direction = 4;
                if (_eB2.Height > PbConsole.Height) _eB2.Direction = 2;

                switch (_eB2.Direction)
                {
                    case 1:
                    {
                        _eB2.RefY = _eB2.RefY - _eB2.MovementDelta;
                        break;
                    } //Move up
                    case 2:
                    {
                        _eB2.RefX = _eB2.RefX - _eB2.MovementDelta;
                        break;
                    } //Move left
                    case 3:
                    {
                        _eB2.RefY = _eB2.RefY + _eB2.MovementDelta;
                        break;
                    } //Move down
                    case 4:
                    {
                        _eB2.RefX = _eB2.RefX + _eB2.MovementDelta;
                        break;
                    } //Move right
                }

                //move to left till the whole boss appears in screen
                if (_eB2.X2 > PbConsole.Width)
                    _eB2.RefX = _eB2.RefX - 3;

                //toggle control burner field for animation
                _eB2.ToggleBurner = !_eB2.ToggleBurner;
            }
        }
        private void ProcessBossHpStatus(byte bossType)
        {
            //Process hp status
            if (bossType==1)
            {
                if (_eB1.CurrHp <= _eB1.HitPerLevel*2 && _eB1.CurrHp > _eB1.HitPerLevel && _eB1.CurrHitLevel == 1)
                    //increase boss gun fire to level 2
                {
                    _eB1.CurrHitLevel++;
                    _eB1.FireLvlBigGun++;
                    _eB1.FireLvlMiniGun++;
                }

                if (_eB1.CurrHp < _eB1.HitPerLevel && _eB1.CurrHitLevel < 3) //increase boss gun fire to level 3
                {
                    _eB1.CurrHitLevel++;
                    _eB1.FireLvlBigGun++;
                    _eB1.FireLvlMiniGun++;
                }

                //Update boss hp display
                LblBossRem.Text = _eB1.CurrHp + @"/" + _eB1.OriginalHp;
            }
            else
            {
                if (_eB2.CurrHp <= _eB2.HitPerLevel * 2 && _eB2.CurrHp > _eB2.HitPerLevel && _eB2.CurrHitLevel == 1)
                //increase boss gun fire to level 2
                {
                    _eB2.CurrHitLevel++;
                    _eB2.FireLvlBigGun++;
                    _eB2.FireLvlMiniGun++;
                }

                if (_eB2.CurrHp < _eB2.HitPerLevel && _eB2.CurrHitLevel < 3) //increase boss gun fire to level 3
                {
                    _eB2.CurrHitLevel++;
                    _eB2.FireLvlBigGun++;
                    _eB2.FireLvlMiniGun++;
                }

                //Update boss hp display
                LblBossRem.Text = _eB2.CurrHp + @"/" + _eB2.OriginalHp;
            }
        }
        private void DestroyMyJetOnBossCollision(byte bossType)
        {
            //Destroy my jet if collide with boss
            if (!_gc.GodMode)
            {
                switch (bossType)
                {
                    case 1:
                    {
                        if (_eB1.RefX <= _mj.X2 && _eB1.X2 >= _mj.RefX
                        && ((_eB1.RefY >= _mj.RefY && _eB1.RefY <= _mj.Y2) || (_eB1.HitY2 >= _mj.RefY && _eB1.HitY2 <= _mj.Y2)))
                        {
                            _exp.Add(new Explosion { Colour = new Pen(Color.Red), VisibilityCounter = 1, Points = Explosion.Explode(_mj.RefX, _mj.RefY) });

                            _mj.Hp--;

                            if (_mj.Hp <= 0) GameOver();
                        }
                        break;
                    }
                    case 2:
                    {
                        if (
                            (_mj.X2 >= _eB2.DamageAreaCT.X && _mj.RefX <= _eB2.DamageAreaCB.X &&
                            ((_mj.RefY >= _eB2.DamageAreaCT.Y && _mj.RefY <= _eB2.DamageAreaCB.Y) || (_mj.Y2 >= _eB2.DamageAreaCT.Y && _mj.Y2 <= _eB2.DamageAreaCB.Y)))
                            ||
                            (_mj.X2 >= _eB2.DamageAreaTT.X && _mj.RefX <= _eB2.DamageAreaTB.X &&
                            ((_mj.RefY >= _eB2.DamageAreaTT.Y && _mj.RefY <= _eB2.DamageAreaTB.Y) || (_mj.Y2 >= _eB2.DamageAreaTT.Y && _mj.Y2 <= _eB2.DamageAreaTB.Y)))
                            ||
                            (_mj.X2 >= _eB2.DamageAreaCT.X && _mj.RefX <= _eB2.DamageAreaCB.X &&
                            ((_mj.RefY >= _eB2.DamageAreaBT.Y && _mj.RefY <= _eB2.DamageAreaBB.Y) || (_mj.Y2 >= _eB2.DamageAreaBT.Y && _mj.Y2 <= _eB2.DamageAreaBB.Y)))
                           )
                        {
                            _exp.Add(new Explosion { Colour = new Pen(Color.Red), VisibilityCounter = 1, Points = Explosion.Explode(_mj.RefX, _mj.RefY) });

                            _mj.Hp--;

                            if (_mj.Hp <= 0) GameOver();
                        }
                        break;
                    }
                }
            }
        }
        private void FireGun_Boss(byte bossType)
        {
            switch (bossType)
            {
                case 1:
                {
                    //Fire mini gun 1
                    if (_rand.Next(0, _eB1.BossMiniGunBulletGenOdd / _eB1.FireLvlMiniGun) == 1)
                    {
                        _ejb.Add(new EnemyBullet { GunFireStart = true, Damage = 2, X = _eB1.MiniGun1X, Y = _eB1.MiniGun1Y, Size = _gc.BossMiniGunBulletSize, InitialX = _eB1.MiniGun1X, InitialY = _eB1.MiniGun1Y, MyJetX = _mj.X2, MyJetY = _mj.Y2 - 5, DeltaX = _eB1.BossMiniGunBulletDelta, Colour = Brushes.DarkRed});
                    }

                    //Fire mini gun 2
                    if (_rand.Next(0, _eB1.BossMiniGunBulletGenOdd / _eB1.FireLvlMiniGun) == 2)
                    {
                        _ejb.Add(new EnemyBullet { GunFireStart = true, Damage = 2, X = _eB1.MiniGun2X, Y = _eB1.MiniGun2Y, Size = _gc.BossMiniGunBulletSize, InitialX = _eB1.MiniGun2X, InitialY = _eB1.MiniGun2Y, MyJetX = _mj.X2, MyJetY = _mj.Y2 - 5, DeltaX = _eB1.BossMiniGunBulletDelta, Colour = Brushes.DarkRed });
                    }

                    //Fire big gun
                    if (_rand.Next(0, _eB1.BossBigGunBulletGenOdd / _eB1.FireLvlMiniGun) == 5)
                    {
                        _ejb.Add(new EnemyBullet { GunFireStart = true, Damage = 4, X = _eB1.BigGunX, Y = _eB1.BigGunY, Size = _gc.BossBigGunBulletSize, InitialX = _eB1.BigGunX, InitialY = _eB1.BigGunY, MyJetX = _mj.X2, MyJetY = _mj.Y2 - 5, DeltaX = _eB1.BossBigGunBulletDelta, ColourPen = new Pen(Color.Red, 2), IsHollowBullet = true });
                    }

                    break;
                }
                case 2:
                {
                    //Fire mini gun 1
                    if (_rand.Next(0, _eB2.BossMiniGunBulletGenOdd / _eB2.FireLvlMiniGun) == 1)
                    {
                        _ejb.Add(new EnemyBullet { GunFireStart = true, Damage = 2, X = _eB2.MiniGun1X, Y = _eB2.MiniGun1Y, Size = _gc.BossMiniGunBulletSize, InitialX = _eB2.MiniGun1X, InitialY = _eB2.MiniGun1Y, MyJetX = _mj.X2, MyJetY = _mj.Y2 - 5, DeltaX = _eB2.BossMiniGunBulletDelta, Colour = Brushes.Firebrick });
                    }

                    //Fire mini gun 2
                    if (_rand.Next(0, _eB2.BossMiniGunBulletGenOdd / _eB2.FireLvlMiniGun) == 2)
                    {
                        _ejb.Add(new EnemyBullet { GunFireStart = true, Damage = 2, X = _eB2.MiniGun2X, Y = _eB2.MiniGun2Y, Size = _gc.BossMiniGunBulletSize, InitialX = _eB2.MiniGun2X, InitialY = _eB2.MiniGun2Y, MyJetX = _mj.X2, MyJetY = _mj.Y2 - 5, DeltaX = _eB2.BossMiniGunBulletDelta, Colour = Brushes.Firebrick });
                    }

                    //Fire big gun
                    if (_rand.Next(0, _eB2.BossBigGunBulletGenOdd / _eB2.FireLvlMiniGun) == 5)
                    {
                        _ejb.Add(new EnemyBullet { GunFireStart = true, Damage = 4, X = _eB2.BigGunX, Y = _eB2.BigGunY, Size = _gc.BossBigGunBulletSize, InitialX = _eB2.BigGunX, InitialY = _eB2.BigGunY, MyJetX = _mj.X2, MyJetY = _mj.Y2 - 5, DeltaX = _eB2.BossBigGunBulletDelta, IsHollowBullet = true, ColourPen = new Pen(Color.Purple, 2) });
                    }
                    
                    break;
                }
            }
        }
        #endregion

        #region BossType1
        private void DrawBossType1CommonParts(Graphics sky)
        {
            sky.FillPolygon(Brushes.DodgerBlue, _eB1.MiddleWingType());
            sky.FillPolygon(Brushes.RoyalBlue, _eB1.FuselageBottom());
            sky.FillPolygon(Brushes.CadetBlue, _eB1.FuselageTop());
            sky.FillPolygon(Brushes.Teal, _eB1.TopFin());
            sky.FillPolygon(Brushes.MidnightBlue, _eB1.BackExhaust());
            sky.FillPolygon(Brushes.DarkRed, _eB1.MiniGunTop());
            sky.FillPolygon(Brushes.DarkRed, _eB1.MiniGunBottom());
            sky.FillPolygon(Brushes.Red, _eB1.BigGun());

            sky.DrawPolygon(new Pen(Color.Blue), _eB1.FuselageMain());
            sky.DrawPolygon(new Pen(Color.SlateGray, 2), _eB1.FuselageBottom());
            sky.DrawPolygon(new Pen(Color.SlateGray, 2), _eB1.FuselageTop());
            sky.DrawPolygon(new Pen(Color.SlateGray, 2), _eB1.TopFin());
        }
        private void DrawBossType1MainFeselage(Graphics sky)
        {
            switch (_eB1.CurrHitLevel) //Change fuselate colour based on hit level
            {
                case 1: { sky.FillPolygon(Brushes.Silver, _eB1.FuselageMain()); break; }
                case 2: { sky.FillPolygon(Brushes.LightPink, _eB1.FuselageMain()); break; }
                case 3: { sky.FillPolygon(Brushes.Orange, _eB1.FuselageMain()); break; }
            }
        }
        private void DrawBossType1BackBurner(Graphics sky)
        {
            //Back burner draw
            if (_eB1.ToggleBurner)
            { sky.FillPolygon(Brushes.Tomato, _eB1.BackBurner()); }
            else
            { sky.FillPolygon(Brushes.Yellow, _eB1.BackBurner()); }
        }
        private void DrawBossType1ExpodingParts(Graphics sky)
        {
            if (_eB1.BossDestroyedFireWorkCounter % 2 == 0)
            {
                _exp.Add(new Explosion { Colour = new Pen(Color.Green), VisibilityCounter = 1, Points = Explosion.Explode(_eB1.RefX + (_eB1.BossDestroyedFireWorkCounter * _eB1.Width / 100), _eB1.HitY1 - 10) });
                sky.FillPolygon(Brushes.Red, _eB1.FuselageMain());

                //continue making sound
                PlaySound(BombXplode);
            }
            else
            {
                _exp.Add(new Explosion { Colour = new Pen(Color.Gray), VisibilityCounter = 1, Points = Explosion.Explode(_eB1.RefX + (_eB1.BossDestroyedFireWorkCounter * _eB1.Width / 100), _eB1.HitY2 - 10) });
                sky.FillPolygon(Brushes.OrangeRed, _eB1.FuselageMain());

                //continue making sound
                PlaySound(BombXplode);
            }
        }
        private void BossType1PostExplosionProcess()
        {
            _eB1.BossDestroyedFireWorkCounter++;

            if (_eB1.BossDestroyedFireWorkCounter >= 100)
            {
                //Enough boss destroy fireworks, put the show to an end

                ExplodeBomb();
                LblBoss.Visible = false;
                LblBossRem.Visible = false;
                LevelCompleted();
            }
        }
        #endregion

        #region BossType2

        private void DrawBossType2ExpodingParts(Graphics sky)
        {
            if (_eB2.BossDestroyedFireWorkCounter % 2 == 0)
            {
                _exp.Add(new Explosion { Colour = new Pen(Color.Green), VisibilityCounter = 1, Points = Explosion.Explode(_eB2.RefX + (_eB2.BossDestroyedFireWorkCounter * _eB2.Width / 100), _eB2.DamageAreaCT.Y - 10) });
                sky.FillPolygon(Brushes.Red, _eB2.MainFuselage());

                //continue making sound
                PlaySound(BombXplode);
            }
            else
            {
                _exp.Add(new Explosion { Colour = new Pen(Color.DarkGray), VisibilityCounter = 1, Points = Explosion.Explode(_eB2.RefX + (_eB2.BossDestroyedFireWorkCounter * _eB2.Width / 100), _eB2.DamageAreaCB.Y - 10) });
                sky.FillPolygon(Brushes.Orange, _eB2.MainFuselage());

                //continue making sound
                PlaySound(BombXplode);
            }

        }
        private void DrawBossType2CommonParts(Graphics sky)
        {
            sky.FillPolygon(Brushes.DarkSlateGray, _eB2.Nose());
            sky.FillPolygon(Brushes.DodgerBlue, _eB2.Head());
            sky.FillPolygon(Brushes.Black, _eB2.Cockpit());

            sky.FillPolygon(Brushes.Lavender, _eB2.MainFuselage());
            sky.FillPolygon(Brushes.OrangeRed, _eB2.SmallGunTop());
            sky.FillPolygon(Brushes.OrangeRed, _eB2.SmallGunBottom());
            sky.FillPolygon(Brushes.LightSteelBlue, _eB2.WingTop());
            sky.FillPolygon(Brushes.LightSteelBlue, _eB2.WingBottom());
            sky.FillPolygon(Brushes.Teal, _eB2.BigGunRail());
            sky.FillPolygon(Brushes.OrangeRed, _eB2.BigGun());
            sky.FillPolygon(Brushes.SlateGray, _eB2.BackExhaust());

            sky.DrawPolygon(new Pen(Color.Black, 1), _eB2.Head());
            sky.DrawPolygon(new Pen(Color.DodgerBlue, 2), _eB2.WingBottom());
            sky.DrawPolygon(new Pen(Color.DodgerBlue, 2), _eB2.WingTop());
            sky.DrawPolygon(new Pen(Color.Black), _eB2.BigGun());
        }
        private void BossType2PostExplosionProcess()
        {
            _eB2.BossDestroyedFireWorkCounter++;

            if (_eB2.BossDestroyedFireWorkCounter >= 100)
            {
                //Enough boss destroy fireworks, put the show to an end

                ExplodeBomb();
                LblBoss.Visible = false;
                LblBossRem.Visible = false;
                LevelCompleted();
            }
        }
        private void DrawBossType2MainFeselage(Graphics sky)
        {
            switch (_eB2.CurrHitLevel) //Change fuselate colour based on hit level
            {
                case 1: { sky.FillPolygon(Brushes.Lavender, _eB2.MainFuselage()); ; break; }
                case 2: { sky.FillPolygon(Brushes.LightCoral, _eB2.MainFuselage()); ; break; }
                case 3: { sky.FillPolygon(Brushes.Orange, _eB2.MainFuselage()); ; break; }
            }
        }
        private void BossType2BigGunMovementProcess()
        {
            _eB2.BigGunMovtCounter++;

            if (_eB2.BigGunMovtCounter <= 200 && _eB2.BigGunMovtCounter % 2 == 0)
            {
                _eB2.BigGunXDelta++;
            }
            else if (_eB2.BigGunMovtCounter > 200 && _eB2.BigGunMovtCounter % 2 == 1)
            {
                _eB2.BigGunXDelta--;
            }

            if (_eB2.BigGunMovtCounter > 400) _eB2.BigGunMovtCounter = 0;
        }
        private void DrawBossType2BackBurner(Graphics sky)
        {
            if (_eB2.ToggleBurner)
            { sky.FillPolygon(Brushes.Tomato, _eB2.BackBurnerLarge()); }
            else
            { sky.FillPolygon(Brushes.Yellow, _eB2.BackBurnerSmall()); }
        }
        private void BossNoseCharge(Graphics sky)
        {
            //get a random timer delay for the gun to be fired
            if (_eB2.NoseGunDelayMax == 0) _eB2.NoseGunDelayMax = _rand.Next(100, 300);

            //countdown timer
            _eB2.NoseGunDelayCounter++;

            //Animate gun charge in nose
            if (_eB2.NoseGunDelayCounter%(_eB2.NoseGunDelayMax/30) == 0)
            {
                _eB2.NoseGunCharge++;
                if (_eB2.NoseGunCharge > 30) _eB2.NoseGunCharge = 30;
                PlaySound(NoseBombCharge);
            }
            
            sky.FillRectangle(Brushes.Red, _eB2.RefX + 1, _eB2.RefY + 44, _eB2.NoseGunCharge, 2 + _eB2.BossSizeDeltaY);
        }
        private void BossNoseGunFire()
        {
            //Add bullets in list
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = -10, X = _eB2.RefX, Y = _eB2.RefY + 45, Size=10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = -8, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = -6, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = -4, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = -2, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });

            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = 0, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });

            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = 2, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = 4, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = 6, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = 8, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });
            _ejb.Add(new EnemyBullet { Colour = Brushes.DarkGoldenrod, Damage = 2, GunFireStart = true, DeltaX = 10, DeltaGunTargetY = 10, X = _eB2.RefX, Y = _eB2.RefY + 45, Size = 10 });

            PlaySound(BombXplode);
            //Reset nose gun attribs
            _eB2.NoseGunInitiated = false;
            _eB2.NoseGunDelayCounter = 0;
            _eB2.NoseGunDelayMax = 0;
            _eB2.NoseGunCharge = 0;
        }
        #endregion

        #region Common
        private void StartRoutine()
        {
            LblScore.Text = _gc.Score.ToString();
            LblJetHp.Text = _mj.Hp.ToString();
            LblBombRem.Text = _mjb.BombRemains.ToString();
            LblFireLimit.Text = _mj.CurrentBulletLimit.ToString();
            LblFireLine.Text = _mj.CurrentActiveGun.ToString();
        }
        private void EndRoutine()
        {
            if (_mj.Hp < 0) _mj.Hp = 0;
            if (_eB1.CurrHp < 0) _eB1.CurrHp = 0;
            if (_eB2.CurrHp < 0) _eB2.CurrHp = 0;
            if (_gc.EnemyJetT1GenOdd < 13) _gc.EnemyJetT1GenOdd = 13;
            if (_gc.EnemyBulletGenOdd < 16) _gc.EnemyBulletGenOdd = 16;
            if (_gc.BossMiniGunBulletGenOdd < 2) _gc.BossMiniGunBulletGenOdd = 2;
            if (_gc.BossBigGunBulletGenOdd < 6) _gc.BossBigGunBulletGenOdd = 6;

        }
        private void GameOver()
        {
            _gc.IsGameOver = true;
            PlaySound(EnemyXplode);
            LblStartEndBanner.Visible = true;
            LblStartEndBanner.Text = @"Game Over!!! Hit enter to start another game.";
            LblStartEndBanner.ForeColor = Color.Red;
            LblInfo.Visible = true;
        }
        private void LevelCompleted()
        {
            if (_gc.LevelTransitionCountDown == 0)
            {
                //Update level related
                _gc.InLevelTransition = true;
                _gc.LevelCompleted++;
                _gc.LevelTransitionCountDown++;

                if (_gc.DayPhase < 4)
                {
                    _gc.DayPhase++;
                }
                else
                {
                    _gc.DayPhase = 1;
                }

                //Clear remaining foes and display banner
                LblStartEndBanner.Visible = true;
                LblStartEndBanner.ForeColor = Color.Teal;
                LblStartEndBanner.Text = @"Level " + _gc.LevelCompleted + @" Clear! Prepare for next level.";

                //Update powerup related
                _gc.PowerUpGenOdd = _gc.PowerUpGenOdd + 100;

                //Update Enemy jet status
                _gc.MaxEnemyJetT1PerLvl++;
                _gc.MaxEnemyJetT2PerLvl++;
                _gc.EnemyJetT1GenOdd = (byte)(_gc.EnemyJetT1GenOdd + 5);               //normal enemy gen goes down
                _gc.EnemyJetT2GenOdd = (byte)(_gc.EnemyJetT2GenOdd - 5);                 //special enemy gen goes up
                _gc.EnemyJetT1BulletDelta++;
                _gc.EnemyJetT2BulletDelta++;
                _gc.EnemyJetT1Hp++;
                _gc.EnemyJetT2Hp++;

                _gc.EnemyBulletGenOdd = (byte)(_gc.EnemyBulletGenOdd - 2);

                //Update Enemy heli status
                _gc.MaxEnemyHeliT1PerLvl++;
                _gc.EnemyHeliT1BulletDelta++;
                _gc.EnemyHeliT1GenOdd--;
                _gc.EnemyHeliT1Hp++;
                _gc.EnemyHeliT1MovementDelta++;
                _gc.EnemyHeliT1BulletPerRound++;

                _gc.BossAppearScore = (int)(_gc.BossAppearScore + _gc.BossAppearInterval * 1.5);

                if (_gc.BossType == 1)
                {
                    _eB1.CurrHp = _eB1.OriginalHp + _gc.BossHpIncrement * _gc.LevelCompleted;
                    _eB1.OriginalHp = _eB1.OriginalHp + _gc.BossHpIncrement * _gc.LevelCompleted;

                    _eB1.BossMiniGunBulletDelta++;
                    _eB1.BossMiniGunBulletSize++;
                    _eB1.BossBigGunBulletDelta++;
                    _eB1.BossBigGunBulletSize++;
                    _eB1.BossBigGunBulletGenOdd = _eB1.BossBigGunBulletGenOdd = (byte)(_eB1.BossBigGunBulletGenOdd - 10);
                    _eB1.BossMiniGunBulletGenOdd = _eB1.BossMiniGunBulletGenOdd = (byte)(_eB1.BossMiniGunBulletGenOdd - 10);
                    _eB1.BossSizeDeltaX = (byte)(_gc.LevelCompleted * 24);
                    _eB1.BossSizeDeltaY = (byte)(_gc.LevelCompleted * 8);

                    _eB1.ResetBasics();
                }
                else
                {
                    _eB2.CurrHp = _eB2.OriginalHp + _gc.BossHpIncrement * _gc.LevelCompleted;
                    _eB2.OriginalHp = _eB2.OriginalHp + _gc.BossHpIncrement * _gc.LevelCompleted;

                    _eB2.BossMiniGunBulletDelta++;
                    _eB2.BossMiniGunBulletSize++;
                    _eB2.BossBigGunBulletDelta++;
                    _eB2.BossBigGunBulletSize++;
                    _eB2.BossBigGunBulletGenOdd = _eB2.BossBigGunBulletGenOdd = (byte)(_eB2.BossBigGunBulletGenOdd - 10);
                    _eB2.BossMiniGunBulletGenOdd = _eB2.BossMiniGunBulletGenOdd = (byte)(_eB2.BossMiniGunBulletGenOdd - 10);
                    _eB2.BossSizeDeltaX = (byte)(_gc.LevelCompleted * 15);
                    _eB2.BossSizeDeltaY = (byte)(_gc.LevelCompleted * 6);

                    _eB2.ResetBasics();
                }

                //Update boss status
                if (_gc.BossType == 2)          //alternate between bosses
                {
                    _gc.BossType = 1;
                }
                else
                {
                    _gc.BossType = 2;
                }
            }
            else
            {
                _gc.LevelTransitionCountDown++;
                if (_gc.LevelTransitionCountDown >= 200)
                {
                    _gc.InLevelTransition = false;
                    _gc.LevelTransitionCountDown = 0;
                    LblStartEndBanner.Visible = false;
                }
            }
        }
        private void LevelAndHpUpdate()
        {
            if (!_eB1.IsBossInitiated)
            {
                if (_gc.Score >= _gc.NextScoreCheckPoint)
                {
                    _gc.NextScoreCheckPoint = _gc.NextScoreCheckPoint + 100;    //update next score check point
                    _mj.Hp++;                                                   //inrease my jet Hp
                }
            }

            if (_gc.Score > _gc.BossAppearScore)
            {
                BossFight();
            }
        }
        private void CleanUp()
        {
            //remove redundant object drawings
            for (int i = 0; i < _cL.Count; i++)
            {
                if (!_cL[i].CloudInitiated) _cL.Remove(_cL[i]);
            }

            for (int i = 0; i < _mjgb.Count; i++)
            {
                if (!_mjgb[i].GunFireStart) _mjgb.Remove(_mjgb[i]);
            }

            for (int i = 0; i < _ej.Count; i++)
            {
                if (_ej[i].MakeRedundant) _ej.Remove(_ej[i]);
            }

            for (int i = 0; i < _eH.Count; i++)
            {
                if (_eH[i].MakeRedundant) _eH.Remove(_eH[i]);
            }

            for (int i = 0; i < _ejb.Count; i++)
            {
                if (!_ejb[i].GunFireStart) _ejb.Remove(_ejb[i]);
            }

            for (int i = 0; i < _exp.Count; i++)
            {
                if (_exp[i].VisibilityCounter == 0) _exp.Remove(_exp[i]);
            }
        }
        private void PlaySound(string uri)
        {
            if (_gc.EnableSound)
            {
                var sound = new MediaPlayer();
                sound.Open(new Uri(uri));
                sound.Play();
            }
        }
        private void MainConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Up))
                _ki.KeyDirectionVertical = 38;

            if (Keyboard.IsKeyDown(Key.Down))
                _ki.KeyDirectionVertical = 40;

            if (Keyboard.IsKeyDown(Key.Left))
                _ki.KeyDirectionHorizontal = 37;
            
            if (Keyboard.IsKeyDown(Key.Right))
                _ki.KeyDirectionHorizontal = 39;

            if (Keyboard.IsKeyDown(Key.A))
                _ki.KeyFire = true;

            if (Keyboard.IsKeyDown(Key.D))
                _ki.KeyBomb = true;

            if (Keyboard.IsKeyDown(Key.Enter))
                _ki.RestartGame = true;
        }
        private void MainConsole_KeyUp(object sender, KeyEventArgs e)
        {
            _ki.KeyDirectionVertical = 0;
            _ki.KeyDirectionHorizontal = 0;
            _ki.KeyFire = false;
            _ki.KeyBomb = false;
        }
        #endregion
    }
}
