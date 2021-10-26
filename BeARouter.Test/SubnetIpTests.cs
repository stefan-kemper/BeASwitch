﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BeARouter.Test
{
    class SubnetIpTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MaskToAddress()
        {
            byte[] mask1 = new byte[] { 224,0,0,0}; 
            var mask1Res = Subnet.MaskToBytes(3);
            Assert.IsTrue(mask1.SequenceEqual(mask1Res));

            byte[] mask2 = new byte[] { 252, 0, 0, 0 };
            var mask2Res = Subnet.MaskToBytes(6);
            Assert.IsTrue(mask2.SequenceEqual(mask2Res));

            byte[] mask3 = new byte[] { 255, 128, 0, 0 };
            var mask3Res = Subnet.MaskToBytes(9);
            Assert.IsTrue(mask3.SequenceEqual(mask3Res));

            byte[] mask4 = new byte[] { 255, 255, 255, 0 };
            var mask4Res = Subnet.MaskToBytes(24);
            Assert.IsTrue(mask4.SequenceEqual(mask4Res));

            byte[] mask5 = new byte[] { 255, 255, 255, 255 };
            var mask5Res = Subnet.MaskToBytes(32);
            Assert.IsTrue(mask5.SequenceEqual(mask5Res));
        }

        private SubnetMatchResult Match(string netmaskStr, int mask, string addressStr)
        {
            var subnet = new Subnet(new IPv4Address(netmaskStr), mask);
            var address = new IPv4Address(addressStr);
            return subnet.Match(address);
        }

        [Test]
        public void SubnetMapTest1()
        {
            Assert.IsTrue(Match("192.168.1.0", 24,  "192.168.1.5").IsMatch);
        }

        [Test]
        public void SubnetMapTest2()
        {
            Assert.IsFalse(Match("192.168.0.0", 24, "192.168.1.5").IsMatch);
        }

        [Test]
        public void SubnetMapTest3()
        {
            Assert.IsTrue(Match("0.0.0.0", 0, "192.168.1.5").IsMatch);
        }

        [Test]
        public void SubnetMapTest4()
        {
            Assert.IsTrue(Match("0.0.0.0", 0, "255.255.255.255").IsMatch);
        }

        [Test]
        public void SubnetMapTest5()
        {
            Assert.IsTrue(Match("10.0.0.0", 8, "10.3.2.23").IsMatch);
        }

        [Test]
        public void SubnetMapTest6()
        {
            Assert.IsTrue(Match("10.0.0.0", 9, "10.3.2.23").IsMatch);
        }

        [Test]
        public void SubnetMapTest7()
        {
            Assert.IsTrue(Match("10.2.0.0", 15, "10.3.2.23").IsMatch);
        }

        [Test]
        public void SubnetMapTest8()
        {
            Assert.IsTrue(Match("10.2.0.0", 15, "10.2.255.255").IsMatch);
        }

        [Test]
        public void SubnetMapTest9()
        {
            Assert.IsTrue(Match("192.168.10.0", 24, "192.168.10.0").IsMatch);
        }

        [Test]
        public void SubnetMapTest10()
        {
            Assert.IsTrue(Match("192.168.10.0", 24, "192.168.10.1").IsMatch);
        }

        [Test]
        public void SubnetMapTest11()
        {
            Assert.IsTrue(Match("192.168.10.0", 24, "192.168.10.255").IsMatch);
        }

        [Test]
        public void SubnetMapTest12()
        {
            Assert.IsFalse(Match("192.168.10.0", 24, "192.168.11.255").IsMatch);
        }
    }
}