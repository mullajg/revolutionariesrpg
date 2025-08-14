import React from "react";
import { Card, CardBody, Input } from "@heroui/react";
import { Icon } from "@iconify/react";

interface AttributeProps {
    name: string;
    abbr: string;
    icon: string;
    defaultValue?: number;
}

const Attribute: React.FC<AttributeProps> = ({ name, abbr, icon, defaultValue = 10 }) => {
    const [value, setValue] = React.useState(defaultValue.toString());
    const modifier = Math.floor((parseInt(value) - 10) / 2);
    const modifierText = modifier >= 0 ? `+${modifier}` : `${modifier}`;

    return (
        <Card className="overflow-visible">
            <CardBody className="flex flex-col items-center p-3">
                <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 mb-2">
                    <Icon icon={icon} className="w-5 h-5 text-primary-500" />
                </div>
                <h3 className="text-sm font-semibold text-slate-700 dark:text-slate-300">{name}</h3>
                <p className="text-xs text-slate-500 dark:text-slate-400 mb-2">{abbr}</p>
                <Input
                    type="number"
                    size="sm"
                    value={value}
                    onValueChange={setValue}
                    className="w-16 text-center"
                    min={1}
                    max={30}
                />
                <div className="mt-2 text-lg font-bold text-primary-600 dark:text-primary-400">
                    {modifierText}
                </div>
            </CardBody>
        </Card>
    );
};

export default function AttributesSection(){
    return (
        <div>
            <h2 className="text-xl font-semibold mb-4 text-slate-800 dark:text-slate-200">Attributes</h2>
            <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
                <Attribute name="Strength" abbr="STR" icon="lucide:dumbbell" defaultValue={16} />
                <Attribute name="Dexterity" abbr="DEX" icon="lucide:activity" defaultValue={14} />
                <Attribute name="Constitution" abbr="CON" icon="lucide:heart" defaultValue={15} />
                <Attribute name="Intelligence" abbr="INT" icon="lucide:brain" defaultValue={12} />
                <Attribute name="Wisdom" abbr="WIS" icon="lucide:eye" defaultValue={13} />
                <Attribute name="Charisma" abbr="CHA" icon="lucide:message-circle" defaultValue={10} />
            </div>
        </div>
    );
}