'use client';

import { title } from "@/components/primitives";
import { Listbox, ListboxItem } from "@heroui/listbox";
import { Card, CardBody, CardFooter, CardHeader } from "@heroui/card";
import { Divider } from "@heroui/divider";
import { GiAk47, GiBouncingSword, GiBookmarklet, GiFairyWand, GiLightBackpack, GiHighKick, GiPerson, GiDwarfHelmet, GiToolbox, GiBullseye } from "react-icons/gi";
import React from 'react';
import { useRouter } from "next/navigation";

export default function CompendiumPage() {
    const router = useRouter();

    const list = [
        {
            title: "Actions",
            icon: <GiBouncingSword size="2rem" className="w-24" />,
            href: "/compendium/actions",
        },
        {
            title: "Attributes",
            icon: <GiPerson size="2rem" className="w-24" />,
            href: "/compendium/attributes",
        },
        {
            title: "Classes",
            icon: <GiToolbox size="2rem" className="w-24" />,
            href: "/compendium/classes",
        },
        {
            title: "Equipment",
            icon: <GiLightBackpack size="2rem" className="w-24" />,
            href: "/compendium/equipment",
        },
        {
            title: "Feats",
            icon: <GiBullseye size="2rem" className="w-24" />,
            href: "/compendium/feats",
        },
        {
            title: "Heritages",
            icon: <GiDwarfHelmet size="2rem" className="w-24" />,
            href: "/compendium/heritages",
        },
        {
            title: "Languages",
            icon: <GiBookmarklet size="2rem" className="w-24" />,
            href: "/compendium/languages",
        },
        {
            title: "Skills",
            icon: <GiHighKick size="2rem" className="w-24" />,
            href: "/compendium/skills",
        },
        {
            title: "Spells",
            icon: <GiFairyWand size="2rem" className="w-24" />,
            href: "/compendium/spells",
        },
        {
            title: "Weapons",
            icon: <GiAk47 size="2rem" className="w-24" />,
            href: "/compendium/weapons",
        },
    ];

    return (
        <div className="gap-10 grid grid-cols-2 sm:grid-cols-5">
            {list.map((item, index) => (
                /* esint-disable no-console */
                <Card key={item.href} style={{ width: '6rem', height: '7rem' }} isPressable shadow="sm" onPress={() => router.push(item.href)}>
                    <CardHeader className="flex gap-3 items-center justify-center">
                        {item.icon}
                    </CardHeader>
                    <Divider />
                    <CardFooter className="flex gap-3 items-center justify-center">
                        <p className="text-sm">{item.title}</p>
                    </CardFooter>
                </Card>
            ))}
        </div>
    )
}
